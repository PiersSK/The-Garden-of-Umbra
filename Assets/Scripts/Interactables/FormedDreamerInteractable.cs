using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormedDreamerInteractable : Interactable
{

    public event EventHandler<DreamerTalkedToEventArgs> OnDreamerTalkedTo;
    public class DreamerTalkedToEventArgs: EventArgs
    {
        public int interactions;
        public string dreamerName;
    }

    [SerializeField] private List<string> dialogueLines;
    [SerializeField] private string dreamerName;

    private int currentIndex = 0;
    private int totalInteractions = 0;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogue;

    private float fadeTimer = 0f;
    [SerializeField] private float showDialogueFor = 2.5f;

    private void Start()
    {
        
    }

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
        totalInteractions++;

        dialogue.text = dialogueLines[currentIndex];
        currentIndex++;
        if (currentIndex >= dialogueLines.Count) currentIndex = 0;
        dialogueBox.SetActive(true);
        fadeTimer = 0f;

        OnDreamerTalkedTo?.Invoke(this, new DreamerTalkedToEventArgs() { interactions = totalInteractions, dreamerName = dreamerName });
    }
}
