using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskInteractable : Interactable
{
    public override bool CanInteract()
    {
        if(InventoryManager.Instance.UnlockedFlasks().Count == 0)
            return Tutorial.Instance.currentStep == 1;

        return true;
    }

    public override void Interact()
    {
        int currentFlasks = InventoryManager.Instance.UnlockedFlasks().Count;

        if (currentFlasks == 0)
        {
            Tutorial.Instance.flaskFound = true;
            PlayerThoughts.Instance.ShowThought("Ahh this'll do! It can only fit small creature shadows but that's a start.", 4f);
        } else if (currentFlasks == 1)
        {
            PlayerThoughts.Instance.ShowThought("Yes! The second flask in my kit allows me to combine multiple creature's shadows into one. Must be something in my notes about that", 4f);
        }

        InventoryManager.Instance.flasks[currentFlasks].flaskUnlocked = true;
        Destroy(gameObject);
    }
}
