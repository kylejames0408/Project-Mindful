using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailInteractable : MonoBehaviour, IInteractable
{
    #region Fields
    // Stores the prompt to display
    [SerializeField] private string _prompt;
    [SerializeField] private Transform vec3Target;
    [SerializeField] private GameObject snail;
    [SerializeField] private GameObject snailShell;

    private bool _interacted = false;
    private bool awoken = false;
    private bool reachedTarget = false;
    public float sizeDecrease = 0.1f;
    public float distanceTreshold = 1.0f;
    #endregion

    #region Properties
    // Stores the local prompt as the InteractionPrompt to scale to other game objects
    public string InteractionPrompt => _prompt;
    public bool Interacted => _interacted;
    #endregion

    #region Methods
    private void Start()
    {
        snail.SetActive(false);

    }

    private void FixedUpdate()
    {
        if (_interacted && !reachedTarget)
        {
            if (Vector3.Distance(transform.position, vec3Target.position) <= 0.1f)
            {
                reachedTarget = true;
            }

            if (!awoken)
            {
                awoken = true;
            }

            snail.transform.position = Vector3.Lerp(snail.transform.position, vec3Target.position, 0.01f);



        }

        // Rotate the road
        if (reachedTarget)
        {

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
            //snailShell.SetActive(false);
            snailShell.GetComponent<MeshRenderer>().enabled = false;
            snailShell.GetComponent<MeshCollider>().enabled = false;
            snail.SetActive(true);
            snail.transform.position = snailShell.transform.position;
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(sizeDecrease);
        }
    }
    #endregion
}
