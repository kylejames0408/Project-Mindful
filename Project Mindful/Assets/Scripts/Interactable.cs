using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SOURCE: https://www.youtube.com/watch?v=THmW4YolDok
/// </summary>
public class Interactable : MonoBehaviour
{
    #region Fields
    // Stores the prompt to display
    [SerializeField] private string _prompt;
    private bool _interacted = false;
    private int parentInteractionCount;
    private gameObject parentObj;
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
            parentObj.GetComponent<InteractionParent>();
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(0.1f);
        }
    }
    #endregion
}
