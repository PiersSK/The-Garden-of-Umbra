using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormedDreamerInteractable : Interactable
{
    [SerializeField] private List<string> dialogueLines;

    private int currentIndex = 0;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogue;

    private float fadeTimer = 0f;
    [SerializeField] private float showDialogueFor = 2.5f;


    private void Update()
    {
        if (dialogueBox.activeSelf)
        {
            fadeTimer += Time.deltaTime;
            if(fadeTimer >= showDialogueFor)
            {
                dialogueBox.SetActive(false);
            }
        }
    }

    public override void Interact()
    {
        dialogue.text = dialogueLines[currentIndex];
        currentIndex++;
        if (currentIndex >= dialogueLines.Count) currentIndex = 0;
        dialogueBox.SetActive(true);
        fadeTimer = 0f;
    }
}
