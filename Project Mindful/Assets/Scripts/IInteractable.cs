using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SOURCE: https://www.youtube.com/watch?v=THmW4YolDok
/// </summary>
public interface IInteractable
{
    // Stores the prompt to display
    public string InteractionPrompt { get; }
    public bool Interacted { get; }

    // Does something when the player interacts
    public void Interact(Interactor interactor);

    // Outlines when the player is near
    public void Outline();
}
