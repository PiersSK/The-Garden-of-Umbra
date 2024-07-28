using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskInteractable : Interactable
{
    public override void Interact()
    {
        int currentFlasks = InventoryManager.Instance.UnlockedFlasks().Count;

        if (currentFlasks == 0)
        {
            Tutorial.Instance.flaskFound = true;
            PlayerThoughts.Instance.ShowThought("Ahh this'll do! It can only fit small creature shadows but that's a start.", 4f);
        }

        InventoryManager.Instance.flasks[currentFlasks].flaskUnlocked = true;
        Destroy(gameObject);
    }
}
