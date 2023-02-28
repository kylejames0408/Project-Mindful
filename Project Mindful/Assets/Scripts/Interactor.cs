using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SOURCE: https://www.youtube.com/watch?v=THmW4YolDok
/// </summary>
public class Interactor : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;
    #endregion

    #region Methods
    private void Update()
    {
        // Get the number of interactables in the player's space
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
    
        // If there is at least one interactable
        if (_numFound > 0)
        {
            // Get the first interactable in the array
            _interactable = _colliders[0].GetComponent<IInteractable>();

            // Ensure that the interactable isn't null
            if (_interactable != null)
            {
                // If the UI is not displayed, show the UI
                if (!_interactionPromptUI.IsDisplayed)
                {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }

                // If the player presses E, interact with the object
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interactable.Interact(this);
                }
            }
        }
        else // If there are no interactables
        {
            // If interactable isn't null, make it null
            if (_interactable != null)
            {
                _interactable = null;
            }

            // If the UI is showing, hide it
            if (_interactionPromptUI.IsDisplayed)
            {
                _interactionPromptUI.Close();
            }
        }
    }

    /// <summary>
    /// Shows the interaction sphere in scene
    /// </summary>
    private void OnDrawGizmos()
    {
        // Draw a red wire sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
    #endregion
}
