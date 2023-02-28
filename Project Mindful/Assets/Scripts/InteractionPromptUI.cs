using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// SOURCE: https://www.youtube.com/watch?v=THmW4YolDok
/// </summary>
public class InteractionPromptUI : MonoBehaviour
{
    #region Fields
    private Camera _mainCam;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private TextMeshProUGUI _promptText;
    public bool IsDisplayed = false;
    #endregion

    #region Methods
    /// <summary>
    /// Initialize fields
    /// </summary>
    private void Start()
    {
        // Get the third person camera
        _mainCam = Camera.main;

        // Hide the UI Panel
        _uiPanel.SetActive(false);
    }

    /// <summary>
    /// Update the UI position to face the camera.
    /// </summary>
    private void LateUpdate()
    {
        // Get the rotation of the camera
        Quaternion rotation = _mainCam.transform.rotation;

        // Change the UI transform
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    /// <summary>
    /// Set up the prompt UI
    /// </summary>
    /// <param name="promptText">The text to display</param>
    public void SetUp(string promptText)
    {
        // Set the prompt text to the input text
        _promptText.text = promptText;

        // Show the UI panel & update display field
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    /// <summary>
    /// Hide the prompt UI
    /// </summary>
    public void Close()
    {
        // Hide the UI panel & update display field
        IsDisplayed = false;
        _uiPanel.SetActive(false);
    }
    #endregion
}
