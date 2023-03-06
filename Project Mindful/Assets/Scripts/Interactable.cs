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
            Debug.Log(parentObj.GetComponent<InteractableParent>().currentInteractions);
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(0.1f);

            if(gameObject.GetComponent<SphereCollider>() != null)
            {
                childObj.GetComponent<SphereCollider>().enabled = false;
            }
            if(gameObject.GetComponent<CapsuleCollider>() != null)
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

    public void DisableOutLine()
    {
        childObj.GetComponent<MaterialInstancer>().SetOutline(0f);
    }
    #endregion
}
