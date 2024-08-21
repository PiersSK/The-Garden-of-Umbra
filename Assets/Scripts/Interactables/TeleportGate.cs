using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : Interactable
{
    public bool isLocked = false;
    [SerializeField] private string destinationName;
    [SerializeField] private Vector3 destinationCoords;

    private void Start()
    {
        promptText = "Go to " + destinationName;
    }

    public override void Interact()
    {
        PlayerController.Instance.TeleportPlayerToPoint(destinationCoords);
    }

    public override bool CanInteract()
    {
        return !isLocked;
    }

}
