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
    
    [SerializeField] bool disableMeshAfter;
    private bool _interacted = false;
    private bool kickedBall = false;
    private float _speed = 5f;
    private int parentInteractionCount;
    private GameObject parentObj;
    private GameObject childObj;
    private MaterialInstancer matInstancer;
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
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(0.1f);

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
    }

    public void DisableOutLine()
    {
        childObj.GetComponent<MaterialInstancer>().SetOutline(0f);
    }
    #endregion
}
