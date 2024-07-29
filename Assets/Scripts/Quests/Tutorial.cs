using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;

    [SerializeField] private GameObject dreamerPrefab;
    [SerializeField] private GameObject questSystem;
    [SerializeField] private TextMeshProUGUI questStepText;
    private NavMeshAgent playerAgent;

    [SerializeField] private List<string> questSteps;
    public int currentStep = 0;

    private DreamerInteractable dreamer;

    //Opening Cutscene
    private Vector3 frontOfGarden = new(0f, 1.5f, -28.93f);
    private bool cutsceneStarted = false;
    private bool atGardenFront = false;
    private bool firstThought = false;
    private bool dreamerSpawned = false;
    private bool secondThought = false;
    private bool cutsceneFinished = false;

    //Quest Steps
    private bool dreamerTalkedTo = false;
    public bool flaskFound = false;
    public bool notesFound = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerAgent = PlayerController.Instance.GetComponent<NavMeshAgent>();
        questSystem.SetActive(false);
        questStepText.text = questSteps[currentStep];
    }

    private void Update()
    {
        if (!cutsceneFinished) OpeningCutscene();
        else CheckToProgress();
    }

    public void OpeningCutscene()
    {
        if (!cutsceneStarted)
        {
            PlayerController.Instance.controller.enabled = false;
            PlayerController.Instance.playerAnimator.SetBool("IsWalking", true);

            UIManager.Instance.SetUIForPreset(UIManager.UIPreset.Cutscene);

            playerAgent.enabled = true;
            playerAgent.stoppingDistance = 0f;
            playerAgent.SetDestination(frontOfGarden);



            cutsceneStarted = true;
        }

        if (Vector3.Distance(PlayerController.Instance.transform.position, frontOfGarden) < 1f && !atGardenFront)
        {
            PlayerController.Instance.playerAnimator.SetBool("IsWalking", false);

            playerAgent.enabled = false;
            atGardenFront = true;
            PlayerThoughts.Instance.ShowThought("Ah, another lovely day in the forest!", 3f);
            firstThought = true;
        }


        if (!PlayerThoughts.Instance.thought.activeSelf && firstThought && !dreamerSpawned)
        {
            GameObject dreamerObj = Instantiate(dreamerPrefab, new Vector3(-6.17f, 1.31f, -27.79f), Quaternion.identity);
            dreamer = dreamerObj.GetComponent<DreamerInteractable>();
            dreamer.OnDreamerTalkedTo += CheckDreamerTalkedTo;
            PlayerController.Instance.playerAnimator.SetTrigger("Surprised");
            PlayerThoughts.Instance.ShowReaction(2f);
            dreamerSpawned = true;
        }

        if (!PlayerThoughts.Instance.reaction.activeSelf && !PlayerThoughts.Instance.thought.activeSelf && dreamerSpawned && !secondThought)
        {
            PlayerThoughts.Instance.ShowThought("Oh my, a dreamer! It's been an age since one of these poor souls wandered through. I should investigate", 4f);
            secondThought = true;
        }

        if (secondThought && !PlayerThoughts.Instance.reaction.activeSelf && !PlayerThoughts.Instance.thought.activeSelf)
        {
            PlayerController.Instance.controller.enabled = true;
            UIManager.Instance.SetUIForPreset(UIManager.UIPreset.Garden);
            cutsceneFinished = true;
        }

    }

    private void CheckDreamerTalkedTo(object sender, DreamerInteractable.DreamerTalkedToEventArgs e)
    {
        dreamer.OnDreamerTalkedTo -= CheckDreamerTalkedTo;
        Invoke("HaveDreamerThought", e.expectedWaitTime);
    }

    private void HaveDreamerThought()
    {
        dreamerTalkedTo = true;
        PlayerThoughts.Instance.ShowThought("Hmmm restless dreamers are only half formed in this world. I need to craft them a shadow... oh no, I need to find my flask and notes!", 5f);
    }

    private void CheckToProgress()
    {
        switch(currentStep)
        {
            case 0:
                if (dreamerTalkedTo) ProgressQuestStep();
                break;
            case 1:
                if (flaskFound && notesFound) ProgressQuestStep();
                break;
            default:
                break;
        }
    }
    private void ProgressQuestStep()
    {
        currentStep++;
        if(currentStep + 1 > questSteps.Count) {
            questSystem.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        questStepText.text = questSteps[currentStep];
    }
}
