using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInteractable : Interactable
{
    [SerializeField] private GameObject notesUIButton;

    public override void Interact()
    {
        Tutorial.Instance.notesFound = true;
        PlayerThoughts.Instance.ShowThought("My notes on shadow crafting! I should work out what creature aspects I need for this dreamer", 4f);
        notesUIButton.SetActive(true);
        Destroy(gameObject);
    }
}
