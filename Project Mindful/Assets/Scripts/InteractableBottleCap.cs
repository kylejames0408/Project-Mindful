using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBottleCap : MonoBehaviour, IInteractable
{
    #region Fields
    // Stores the prompt to display
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject rose;
    [SerializeField] private GameObject bottleCap;

    private bool _interacted = false;
    private Vector3 finalPosition;
    public float sizeDecrease = 0.1f;
    bool reachedMaxSize = false;
    
    #endregion

    #region Properties
    // Stores the local prompt as the InteractionPrompt to scale to other game objects
    public string InteractionPrompt => _prompt;
    public bool Interacted => _interacted;
    #endregion

    #region Methods
    private void Start()
    {
        finalPosition = new Vector3(rose.transform.position.x, rose.transform.position.y + 0.9f, rose.transform.position.z);
        rose.SetActive(false);
        
    }

    private void FixedUpdate()
    {
        if (_interacted && !reachedMaxSize)
        {
            if (Vector3.Distance(rose.transform.position, finalPosition) <= 0.1f)
            {
                reachedMaxSize = true;
            }

           

            rose.transform.position = Vector3.Lerp(rose.transform.position, finalPosition, 0.01f);



        }


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
            bottleCap.GetComponent<MeshRenderer>().enabled = false;
            bottleCap.GetComponent<MeshCollider>().enabled = false;
            rose.SetActive(true);

            //snail.transform.position = snailShell.transform.position;
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(sizeDecrease);
        }
    }
    #endregion
}
