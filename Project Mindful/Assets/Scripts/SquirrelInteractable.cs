using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelInteractable : MonoBehaviour, IInteractable
{
    #region Fields
    // Stores the prompt to display
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject acornOnGround;
    [SerializeField] private GameObject acornInHand;

    private bool _interacted = false;
    private bool nutGiven = false;
    public float sizeDecrease = 0.1f;

    #endregion

    #region Properties
    // Stores the local prompt as the InteractionPrompt to scale to other game objects
    public string InteractionPrompt => _prompt;
    public bool Interacted => _interacted;
    #endregion

    #region Methods
    private void Start()
    {
        acornInHand.GetComponent<MeshRenderer>().enabled = false;
    }

    private void FixedUpdate()
    {
        
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
            //snailShell.SetActive(false);
            acornOnGround.GetComponent<MeshRenderer>().enabled = false;
            acornOnGround.GetComponent<MeshCollider>().enabled = false;
            acornInHand.GetComponent<MeshRenderer>().enabled = true;
            //snail.transform.position = snailShell.transform.position;
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(sizeDecrease);
        }
    }
    #endregion
}
