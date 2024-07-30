using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGardenGate : Interactable
{
    public override void Interact()
    {
        Debug.Log(gameObject.name);
        if (QuestTracker.Instance is null || QuestTracker.Instance.questIndex < 2)
            PlayerThoughts.Instance.ShowThought("It's so overgrown, I don't want to think about it right now...", 2f);
        else
        {
            gameObject.SetActive(false);
            PlayerThoughts.Instance.ShowThought("Sometimes you have to face your gardening based fears. I'm coming lil' guy", 2f);
        }
    }
}
