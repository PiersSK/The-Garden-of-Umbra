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
        public float expectedWaitTime;
    }

    [SerializeField] private bool cycleDialogue = true;

    [SerializeField] private List<string> dialogueLines;
    [SerializeField] private string dreamerName;

    private int currentIndex = 0;
    private int totalInteractions = 0;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject dialoguePrefab;
    [SerializeField] private TextMeshProUGUI dialogue;

    private float fadeTimer = 0f;
    [SerializeField] private float showDialogueFor = 2.5f;

    private void Start()
    {
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (dialogueBox.activeSelf)
        {
            fadeTimer += Time.deltaTime;
            if(fadeTimer >= showDialogueFor * (cycleDialogue ? 1 : dialogueLines.Count))
            {
                dialogueBox.SetActive(false);
            }
        }
    }

    public override void Interact()
    {
        totalInteractions++;
        UpdateDialogue();
        OnDreamerTalkedTo?.Invoke(this, new DreamerTalkedToEventArgs() { 
            interactions = totalInteractions,
            dreamerName = dreamerName,
            expectedWaitTime = showDialogueFor * (cycleDialogue ? 1 : dialogueLines.Count)
        });
    }

    private void UpdateDialogue()
    {
        foreach (Transform dialogue in dialogueBox.transform) Destroy(dialogue.gameObject);

        if (cycleDialogue)
        {
            GameObject dialogueObj = Instantiate(dialoguePrefab, dialogueBox.transform);
            dialogueObj.GetComponentInChildren<TextMeshProUGUI>().text = dialogueLines[currentIndex];
            currentIndex++;
            if (currentIndex >= dialogueLines.Count) currentIndex = 0;

        } else
        {
            foreach(string line in dialogueLines)
            {
                GameObject dialogueObj = Instantiate(dialoguePrefab, dialogueBox.transform);
                dialogueObj.GetComponentInChildren<TextMeshProUGUI>().text = line;
            }

        }

        fadeTimer = 0f;
        dialogueBox.SetActive(true);
    }
}
