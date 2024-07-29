using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DreamerInteractable : Interactable
{

    public event EventHandler<DreamerTalkedToEventArgs> OnDreamerTalkedTo;
    public class DreamerTalkedToEventArgs: EventArgs
    {
        public int interactions;
        public string dreamerName;
        public float expectedWaitTime;
    }

    [SerializeField] private bool manuallyCycleDialogue = true;

    private List<string> dialogueLines;
    [SerializeField] private string dreamerName;
    [SerializeField] private Quest associatedQuest;

    private bool isFormed = false;

    private int currentIndex = 0;
    private int totalInteractions = 0;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject dreamDialoguePrefab;
    [SerializeField] private GameObject formedDialoguePrefab;

    private float fadeTimer = 0f;
    private float showDialogueFor;
    [SerializeField] private AnimationClip dialogueFadeAnimation;
    [SerializeField] private Animator smokeAnim;

    private void Start()
    {
        dialogueBox.SetActive(false);
        dialogueLines = associatedQuest.dreamDialogue;
        showDialogueFor = dialogueFadeAnimation.length;
    }

    private void Update()
    {

        if (dialogueBox.activeSelf)
        {
            fadeTimer += Time.deltaTime;
            if (manuallyCycleDialogue && fadeTimer >= showDialogueFor)
            {
                dialogueBox.SetActive(false);
            }
            else if (!manuallyCycleDialogue && fadeTimer >= showDialogueFor * 0.66f + showDialogueFor * currentIndex)
            {
                currentIndex++;

                if (currentIndex < dialogueLines.Count)
                {
                    GameObject prefab = isFormed ? formedDialoguePrefab : dreamDialoguePrefab;
                    GameObject dialogueObj = Instantiate(prefab, dialogueBox.transform);
                    dialogueObj.GetComponentInChildren<TextMeshProUGUI>().text = dialogueLines[currentIndex];
                }
            }
            else if (!manuallyCycleDialogue && fadeTimer > showDialogueFor * dialogueLines.Count)
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
            expectedWaitTime = showDialogueFor * (manuallyCycleDialogue ? 1 : dialogueLines.Count)
        });

        if(QuestTracker.Instance != null && QuestTracker.Instance.activeQuest == associatedQuest)
        {
            if(QuestTracker.Instance.CompleteQuestIfRequirementsMet())
            {
                smokeAnim.SetTrigger("Puff");
                GetComponent<Animation>().Stop();

                GetComponent<SpriteRenderer>().sprite = associatedQuest.questGiver.formedSprite;
                GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                isFormed = true;
                dialogueLines = new() { associatedQuest.completionDialogue };
                UpdateDialogue();

                Invoke("AddFormedLines", showDialogueFor + 1f);
            }
        } 
    }

    private void AddFormedLines()
    {
        manuallyCycleDialogue = true;
        dialogueLines = associatedQuest.formedDialogue;
    }

    private void UpdateDialogue()
    {
        foreach (Transform dialogue in dialogueBox.transform) Destroy(dialogue.gameObject);

        GameObject prefab = isFormed ? formedDialoguePrefab : dreamDialoguePrefab;

        if (manuallyCycleDialogue)
        {
            GameObject dialogueObj = Instantiate(prefab, dialogueBox.transform);
            dialogueObj.GetComponentInChildren<TextMeshProUGUI>().text = dialogueLines[currentIndex];
            currentIndex++;
            if (currentIndex >= dialogueLines.Count) currentIndex = 0;

        } else
        {
            GameObject dialogueObj = Instantiate(prefab, dialogueBox.transform);
            currentIndex = 0;
            dialogueObj.GetComponentInChildren<TextMeshProUGUI>().text = dialogueLines[currentIndex];

        }

        fadeTimer = 0f;
        dialogueBox.SetActive(true);
    }
}
