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

    private void Start()
    {
        SceneManager.activeSceneChanged += SearchForTargetUIIfLost;
    }

    private void Update()
    {
        UpdateGatheringList();
        Debug.Log(QuestRequirementsMet());
    }

    private void SearchForTargetUIIfLost(Scene current, Scene next)
    {
        if (targetUI == null)
        {
            try
            {
                targetUI = GameObject.Find(targetUIName).GetComponent<TextMeshProUGUI>();
            }
            catch
            {
                Debug.LogWarning("Can't find QuestTracker targetUI");
            }
        }
    }

    private void UpdateGatheringList()
    {
        if (targetUI != null)
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
    }

    public bool QuestRequirementsMet()
    {
        return headAspect == activeQuest.headSolution
            && bodyAspect == activeQuest.bodySolution
            && feetAspect == activeQuest.feetSolution;
    }


}
