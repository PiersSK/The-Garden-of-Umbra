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
    public int questIndex = 0;
    public Quest activeQuest;

    [Header("Resultant Shadow")]
    [SerializeField] private MixShadowUI combinedShadow;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI targetUI;
    [SerializeField] private ChecklistUI checklistUI;
    [SerializeField] private TextMeshProUGUI dreamerReminderUI;
    [SerializeField] private string targetUIName;

    [Header("Unlockable Objects")]
    public GameObject secondFlask;


    private DreamerInteractable currentDreamer;

    // Aspects To Gather
    public HeadAspect headToGather = HeadAspect.None;
    public BodyAspect bodyToGather = BodyAspect.None;
    public FeetAspect feetToGather = FeetAspect.None;


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
        UpdateUnlockables();
    }

    private void UpdateGatheringList()
    {
        if (targetUI != null)
        {
            if (activeQuest != null)
            {
                checklistUI.dreamerNote.text = string.Join("\n", activeQuest.dreamDialogue);

                if (NumberOfAspectsChecked() > 0)
                {
                    string quest = "You have set out to make a <b>" + GetShadowName() + "</b>. You'll need to craft a shadow with:";
                    if (headToGather != HeadAspect.None) quest += "\n\t - " + headToGather.ToString();
                    if (bodyToGather != BodyAspect.None) quest += "\n\t - " + bodyToGather.ToString();
                    if (feetToGather != FeetAspect.None) quest += "\n\t - " + feetToGather.ToString();

                    targetUI.text = quest;
                }
                else
                {
                    targetUI.text = "Talk to the dreamer and work out what shadow you need to craft.\nIf your crafted shadow is correct you can talk to the dreamer again to apply it to them\nHint: Use your shadowcrafting notes to work it out and make a checklist!";
                }
            }
            else
            {
                targetUI.text = "Wait for a new dreamer to arrive...";
            }
        }
    }

    private void UpdateUnlockables()
    {
        if (secondFlask is not null)
        {
            if (questIndex == 1 && !secondFlask.activeSelf)
            {
                secondFlask.SetActive(true);

            }
        }
    }

    private void DreamerResponseThoughts(object caller, DreamerInteractable.DreamerTalkedToEventArgs e)
    {
        currentDreamer.OnDreamerTalkedTo -= DreamerResponseThoughts;
        Invoke("DreamerThoughts", e.expectedWaitTime);
    }

    private void DreamerThoughts()
    {
        if (questIndex == 1)
            PlayerThoughts.Instance.ShowThought("Hmm this may require some proper shadowcraft... I need to find my other lost shadow flasks (and preferably stop dropping them)", 5f);
    }

    private int NumberOfAspectsChecked()
    {
        return headToGather != HeadAspect.None ? 1 : 0
            + bodyToGather != BodyAspect.None ? 1 : 0
            + feetToGather != FeetAspect.None ? 1: 0;
    }

    public bool QuestRequirementsMet()
    {
        Shadow shadow = combinedShadow.combinedShadow;
        if (shadow is null) return false;

        return shadow.headAspect == activeQuest.headSolution
            && shadow.bodyAspect == activeQuest.bodySolution
            && shadow.feetAspect == activeQuest.feetSolution;
    }

    public bool CompleteQuestIfRequirementsMet()
    {
        if(QuestRequirementsMet())
        {
            InventoryManager.Instance.ClearAllFlasksAndRespawnShadows();

            checklistUI.UncheckAll();

            questIndex++;
            if (quests.Count > questIndex)
            {
                activeQuest = quests[questIndex];
                GameObject obj = Instantiate(activeQuest.questGiver.spriteObject, activeQuest.questGiver.spawnPoint, Quaternion.identity);
                currentDreamer = obj.GetComponentInChildren<DreamerInteractable>();
                currentDreamer.OnDreamerTalkedTo += DreamerResponseThoughts;
                currentDreamer.gameObject.name = "Formless Dreamer";
            }
            else
                activeQuest = null;

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
