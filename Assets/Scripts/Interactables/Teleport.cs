using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactable
{
    [SerializeField] private Transform movePlayerTo;
    public override void Interact()
    {
        PlayerController player = PlayerController.Instance;
        player.controller.enabled = false;
        player.transform.position = movePlayerTo.position;
        player.controller.enabled = true;
    }
}
