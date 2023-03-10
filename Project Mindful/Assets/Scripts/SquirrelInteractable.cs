using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelInteractable : MonoBehaviour
{
    #region Fields
    // Stores the prompt to display
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject discardedNut;
    [SerializeField] private GameObject nutInHand;

    private bool _interacted = false;
    private bool nutGiven = false;
    public float sizeDecrease = 0.1f;
    public float distanceTreshold = 1.0f;

    private Vector3 target;
    #endregion

    #region Properties
    // Stores the local prompt as the InteractionPrompt to scale to other game objects
    public string InteractionPrompt => _prompt;
    public bool Interacted => _interacted;
    #endregion

    #region Methods
    private void Start()
    {
        nutInHand.SetActive(false);
        target = nutInHand.transform.position;
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
            discardedNut.GetComponent<MeshRenderer>().enabled = false;
            discardedNut.GetComponent<MeshCollider>().enabled = false;
            nutInHand.SetActive(true);
            //snail.transform.position = snailShell.transform.position;
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(sizeDecrease);
        }
    }
    #endregion
}
