using Aspects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTracker : MonoBehaviour
{
    public static QuestTracker Instance { get; private set; }

    [SerializeField] private List<Quest> quests;
    private int questIndex = 0;
    private Quest activeQuest;

    [SerializeField] private TextMeshProUGUI targetUI;
    [SerializeField] private TextMeshProUGUI dreamerReminderUI;
    [SerializeField] private string targetUIName;

    // Aspects To Gather
    public Aspects.HeadAspect headToGather = Aspects.HeadAspect.None;
    public Aspects.BodyAspect bodyToGather = Aspects.BodyAspect.None;
    public Aspects.FeetAspect feetToGather = Aspects.FeetAspect.None;

    // TEMP TESTING INVENTORY
    public Aspects.HeadAspect headAspect = Aspects.HeadAspect.None;
    public Aspects.BodyAspect bodyAspect = Aspects.BodyAspect.None;
    public Aspects.FeetAspect feetAspect = Aspects.FeetAspect.None;

    private readonly List<string> prefixes = new List<string>() { string.Empty, "Crystal-clear", "Hearty", "Swirling" };
    private readonly List<string> adjectives = new List<string>() { string.Empty, "Protective ", "Pure ", "Inspiring " };
    private readonly List<string> nouns = new List<string>() { string.Empty, "Protection", "Purity", "Inspiration" };
    private readonly List<string> suffixes = new List<string>() { string.Empty, "Embers", "Aura", "Hooves" };


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(quests.Count > 0) 
            activeQuest = quests[0];
    }

    private void Update()
    {
        UpdateGatheringList();
        if(dreamerReminderUI != null && activeQuest != null)
        {
            dreamerReminderUI.text = activeQuest.dreamDialogue;
        }
        if(DialogueUI.Instance != null && activeQuest != null)
            DialogueUI.Instance.SetCurrentDialogueLine(QuestRequirementsMet() ? activeQuest.completionDialogue : activeQuest.dreamDialogue);
    }
    private void UpdateGatheringList()
    {
        if (targetUI != null)
        {
            if (activeQuest != null)
            {
                if (NumberOfAspectsChecked() > 0)
                {
                    string quest = "You have set out to make a <b>" + GetShadowName() + "</b>. You'll need to craft a shadow with:";
                    if (headToGather != Aspects.HeadAspect.None) quest += "\n\t - " + headToGather.ToString();
                    if (bodyToGather != Aspects.BodyAspect.None) quest += "\n\t - " + bodyToGather.ToString();
                    if (feetToGather != Aspects.FeetAspect.None) quest += "\n\t - " + feetToGather.ToString();

                    targetUI.text = quest;
                }
                else
                {
                    targetUI.text = "Talk to the dreamer and work out what shadow you need to craft. Hint: Use your shadowcrafting notes to work it out and make a checklist!";
                }
            }
            else
            {
                targetUI.text = "Wait for a new dreamer to arrive...";
            }
        }
    }

    private int NumberOfAspectsChecked()
    {
        return headToGather != Aspects.HeadAspect.None ? 1 : 0
            + bodyToGather != Aspects.BodyAspect.None ? 1 : 0
            + feetToGather != Aspects.FeetAspect.None ? 1: 0;
    }

    public bool QuestRequirementsMet()
    {
        return headAspect == activeQuest.headSolution
            && bodyAspect == activeQuest.bodySolution
            && feetAspect == activeQuest.feetSolution;
    }

    private void ResetPlayerAspectsAndChecklist()
    {
        headAspect = Aspects.HeadAspect.None;
        bodyAspect = Aspects.BodyAspect.None;
        feetAspect = Aspects.FeetAspect.None;

        ChecklistUI.Instance.UncheckAll();
    }

    public void CompleteQuestIfRequirementsMet()
    {
        if(QuestRequirementsMet())
        {
            Debug.Log("Completing quest");
            InventoryManager.Instance.ClearAllFlasks();
            DialogueUI.Instance.HideDreamerAndDialogue();
            GameObject obj = Instantiate(activeQuest.questGiver.spriteObject, activeQuest.questGiver.spawnPoint, Quaternion.identity);
            obj.name = activeQuest.questGiver.dreamerName;

            ResetPlayerAspectsAndChecklist();

            questIndex++;
            if (quests.Count > questIndex)
                activeQuest = quests[questIndex];    
            else
                activeQuest = null;


        }
    }

    private string GetShadowName()
    {
        string prefix = prefixes[(int)QuestTracker.Instance.headToGather];
        string adjective = string.Empty;
        string noun = string.Empty;
        string suffix = suffixes[(int)QuestTracker.Instance.feetToGather];

        if (QuestTracker.Instance.feetToGather != FeetAspect.None)
            adjective = adjectives[(int)QuestTracker.Instance.bodyToGather];
        else
            noun = nouns[(int)QuestTracker.Instance.bodyToGather];

        string potionName = prefix;
        potionName += prefix == string.Empty ? "Shadow" : " shadow";
        potionName += (adjective + noun + suffix) == string.Empty ? "" : " of ";
        potionName += adjective + noun + suffix;

        return potionName;
    }

}
