using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SOURCE: https://www.youtube.com/watch?v=THmW4YolDok
/// </summary>
public class Interactable : MonoBehaviour, IInteractable
{
    #region Fields
    // Stores the prompt to display
    [SerializeField] private string _prompt;
    [SerializeField] public float sizeDecreaseFactor;
    [SerializeField] bool disableMeshAfter;
    [SerializeField] GameObject flowerBee;
    private bool _interacted = false;
    private bool kickedBall = false;
    private float _speed = 5f;
    private int parentInteractionCount;
    private GameObject parentObj;
    private GameObject childObj;
    private MaterialInstancer matInstancer;
    private bool beeActive = false;
    private Vector3 beePos;
    private float beeFlight = 0.25f;
    private float flightDiection = 1f;

    #endregion

    #region Properties
    // Stores the local prompt as the InteractionPrompt to scale to other game objects
    public string InteractionPrompt => _prompt;
    public bool Interacted => _interacted;
    #endregion

    #region Methods
    /// <summary>
    /// Initialize Fields.
    /// </summary>
    void Start()
    {
        parentObj = this.transform.parent.gameObject;
        childObj = this.transform.GetChild(0).gameObject;
        if(flowerBee != null ) { beePos = flowerBee.transform.position; flowerBee.SetActive(false); }
        matInstancer = gameObject.GetComponent<MaterialInstancer>();
    }
    /// <summary>
    /// Shrinks the player on interaction.
    /// </summary>
    /// <param name="interactor">The player</param>
    public void Interact(Interactor interactor)
    {
        if (!_interacted)
        {
            _interacted = true;
            parentObj.GetComponent<InteractableParent>().currentInteractions += 1;
            if(gameObject.name == "SoccerBall"){
                kickedBall = true;
            }
            Debug.Log(parentObj.GetComponent<InteractableParent>().currentInteractions);
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(sizeDecreaseFactor);

            if(childObj.GetComponent<SphereCollider>() != null)
            {
                childObj.GetComponent<SphereCollider>().enabled = false;
            }
            if(childObj.GetComponent<CapsuleCollider>() != null)
            {
                childObj.GetComponent<CapsuleCollider>().enabled = false;
            }
            if (disableMeshAfter)
            {
                gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            childObj.GetComponent<MaterialInstancer>().SetOutline(0f);

            if (flowerBee != null)
            {
                flowerBee.SetActive(true);
                beeActive = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (kickedBall)
        {
            Vector3 movement = new Vector3(1, 0, 0);
            gameObject.GetComponent<Rigidbody>().AddForce(movement * _speed);

            _speed -= Time.deltaTime;

            if (_speed <= -2f)
            {
                gameObject.GetComponent<Rigidbody>().Sleep();
                this.enabled = false;
            }
        }

        if (beeActive)
        {
            flowerBee.transform.position = new Vector3(flowerBee.transform.position.x, flowerBee.transform.position.y + (0.25f * flightDiection * Time.deltaTime), flowerBee.transform.position.z);

            float beeMax = beePos.y + (beeFlight);
            float beeMin = beePos.y - (beeFlight);
            if (flowerBee.transform.position.y > beeMax)
            {
                flightDiection = -1.0f;
            }

            if (flowerBee.transform.position.y < beeMin)
            {
                flightDiection = 1.0f;
            }
        }
    }



    public void DisableOutLine()
    {
        childObj.GetComponent<MaterialInstancer>().SetOutline(0f);
    }
    #endregion
}
