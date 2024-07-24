using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTracker : MonoBehaviour
{
    public static QuestTracker Instance { get; private set; }

    public Quest activeQuest;

    public List<ShadowAspect> checkedAspects = new();
    [SerializeField] private TextMeshProUGUI targetUI;
    [SerializeField] private string targetUIName;

    // TEMP TESTING INVENTORY
    public ShadowAspect.Aspect headAspect = ShadowAspect.Aspect.None;
    public ShadowAspect.Aspect bodyAspect = ShadowAspect.Aspect.None;
    public ShadowAspect.Aspect feetAspect = ShadowAspect.Aspect.None;


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
    private void Update()
    {
        UpdateGatheringList();
        if(DialogueUI.Instance != null && activeQuest != null)
            DialogueUI.Instance.SetCurrentDialogueLine(QuestRequirementsMet() ? activeQuest.completionDialogue : activeQuest.dreamDialogue);
    }
    private void UpdateGatheringList()
    {
        if (targetUI != null)
        {
            if (activeQuest != null)
            {
                if (checkedAspects.Count > 0)
                {
                    string quest = "You have set out to make a <b>" + ChecklistUI.Instance.GetPotionName() + "</b>";
                    if (checkedAspects.Count == 1)
                        quest += "\nThis will require finding a creature that has just the aspect of <b>" + checkedAspects[0].aspect.ToString() + "</b>";
                    else
                    {
                        quest += "\nThis will require finding a multi-aspected creature or combining shadows to produce one with the following aspects:\n\t<b>";
                        foreach (ShadowAspect markedAspect in checkedAspects) quest += markedAspect.aspect.ToString() + "\n\t";
                        quest += "</b>";
                    }

                    targetUI.text = quest;
                }
                else
                {
                    targetUI.text = "Go to your hut to see what the dreamer needs and add shadows to your list";
                }
            }
            else
            {
                targetUI.text = "Wait for a new dreamer to arrive...";
            }
        }
    }

    public bool QuestRequirementsMet()
    {
        return headAspect == activeQuest.headSolution
            && bodyAspect == activeQuest.bodySolution
            && feetAspect == activeQuest.feetSolution;
    }

    public void CompleteQuestIfRequirementsMet()
    {
        if(QuestRequirementsMet())
        {
            activeQuest = null;
            DialogueUI.Instance.HideDreamerAndDialogue();

        }
    }

}
