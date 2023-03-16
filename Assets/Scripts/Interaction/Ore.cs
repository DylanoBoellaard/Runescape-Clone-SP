using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    // interactor can be used as a check. So an inventory check to see if you have the key for the door
    public bool Interact(Interactor interactor)
    {
        // Do something with the ore
        Debug.Log("Ore mined!");
        return true;
    }
}
