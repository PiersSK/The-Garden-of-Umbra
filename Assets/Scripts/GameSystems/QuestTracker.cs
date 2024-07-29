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
    public Quest activeQuest;

    [SerializeField] private TextMeshProUGUI targetUI;
    [SerializeField] private ChecklistUI checklistUI;
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
        Flask flask = InventoryManager.Instance.flasks[0]; // Temp just check small flask

        return flask.shadow.headAspect == activeQuest.headSolution
            && flask.shadow.bodyAspect == activeQuest.bodySolution
            && flask.shadow.feetAspect == activeQuest.feetSolution;
    }

    private void ResetPlayerAspectsAndChecklist()
    {
        headAspect = Aspects.HeadAspect.None;
        bodyAspect = Aspects.BodyAspect.None;
        feetAspect = Aspects.FeetAspect.None;

        checklistUI.UncheckAll();
    }

    public bool CompleteQuestIfRequirementsMet()
    {
        if(QuestRequirementsMet())
        {
            InventoryManager.Instance.ClearAllFlasks();

            ResetPlayerAspectsAndChecklist();

            questIndex++;
            if (quests.Count > questIndex)
                activeQuest = quests[questIndex];    
            else
                activeQuest = null;

            // spawn in new dreamer

            return true;
        }

        return false;
    }

    private string GetShadowName()
    {
        string prefix = prefixes[(int)headToGather];
        string adjective = string.Empty;
        string noun = string.Empty;
        string suffix = suffixes[(int)feetToGather];

        if (feetToGather != FeetAspect.None)
            adjective = adjectives[(int)bodyToGather];
        else
            noun = nouns[(int)bodyToGather];

        string potionName = prefix;
        potionName += prefix == string.Empty ? "Shadow" : " shadow";
        potionName += (adjective + noun + suffix) == string.Empty ? "" : " of ";
        potionName += adjective + noun + suffix;

        return potionName;
    }

}
