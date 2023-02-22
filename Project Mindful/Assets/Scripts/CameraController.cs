using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Fields
    // fields
    private bool _third;

    // serialized objects
    [SerializeField] GameObject firstPerson;
    [SerializeField] GameObject thirdPerson;

    // player transforms
    private Transform _fpPlayer;
    private Transform _tpPlayer;

    // third person controller
    private StarterAssets.ThirdPersonController _tpController;
    #endregion

    #region Methods
    /// <summary>
    /// Initialize Fields.
    /// </summary>
    void Start()
    {
        // Start player in third person
        _third = true;

        // Ensure correct cameras/players are enabled
        if (firstPerson.activeInHierarchy)
        {
            firstPerson.SetActive(false);
            thirdPerson.SetActive(true);
        }

        // Get the first and third person characters from the serialized fields
        _fpPlayer = firstPerson.transform.Find("Player").gameObject.transform;
        _tpPlayer = thirdPerson.transform.Find("Player").gameObject.transform;

        // Grab the third person controller from the third person player
        _tpController = _tpPlayer.GetComponent<StarterAssets.ThirdPersonController>();
    }

    /// <summary>
    /// Update the camera/player type on V key press.
    /// </summary>
    void Update()
    {
        // If the player presses V
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Change to/from third person
            _third = !_third;

            // If changed to third person
            if (_third)
            {
                // Update the third person player position & rotation
                _tpPlayer.position = _fpPlayer.position;
                _tpPlayer.rotation = _fpPlayer.rotation;

                // Update the third person camera rotation
                _tpController.CinemachineTargetYaw = _fpPlayer.rotation.eulerAngles.y;
                _tpController.CinemachineTargetPitch = _fpPlayer.rotation.eulerAngles.x;

                // Disable the first person mode, enable third person
                firstPerson.SetActive(false);
                thirdPerson.SetActive(true);
            }
            else // if changed to first person
            {
                // Update the first person player position & rotation
                _fpPlayer.position = _tpPlayer.position;
                _fpPlayer.rotation = _tpPlayer.rotation;

                // Disable the third person mode, enable first person
                thirdPerson.SetActive(false);
                firstPerson.SetActive(true);
            }
        }
    }
    #endregion
}
