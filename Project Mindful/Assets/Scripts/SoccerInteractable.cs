using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerInteractable : MonoBehaviour, IInteractable
{
    #region Fields
    // Stores the prompt to display
    [SerializeField] private string _prompt;
    private bool _interacted = false;
    private float _speed;
    #endregion

    #region Properties
    // Stores the local prompt as the InteractionPrompt to scale to other game objects
    public string InteractionPrompt => _prompt;
    public bool Interacted => _interacted;
    #endregion

    #region Methods
    private void Start()
    {
        _speed = 5;
    }

    private void FixedUpdate()
    {
        if (_interacted)
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

    /// <summary>
    /// Shrinks the player on interaction.
    /// </summary>
    /// <param name="interactor">The player</param>
    public void Interact(Interactor interactor)
    {
        if (!_interacted)
        {
            _interacted = true;
            GameObject.Find("SizeManager").GetComponent<SizeManager>().DecreaseScale(0.1f);
        }
    }
    #endregion
}
