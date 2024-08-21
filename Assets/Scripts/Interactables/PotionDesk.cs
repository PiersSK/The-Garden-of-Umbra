using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDesk : Interactable
{
    private int interactions = 0;
    private bool readyForMorePotions = false;

    private void Start()
    {
        promptText = "Pickup Equipment";
    }

    public override void Interact()
    {
        if(interactions == 0)
        {
            Debug.Log("Give player first bottle and notes");
            promptText = "Look At Desk";
            interactions++;
        } else if (interactions == 1 && readyForMorePotions)
        {
            Debug.Log("Should give player other potion bottles");
            promptText = "Look At Desk";
            interactions++;
        } else
        {
            Debug.Log("Default interact");
        }
    }

    public void SetPlayerReadyForMorePotions()
    {
        readyForMorePotions = true;
        promptText = "Pickup Equipment";
    }
}
