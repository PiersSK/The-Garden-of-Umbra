using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI currentLine;
    [SerializeField] private Image dreamerSprite;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button dismissButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dismissButton.onClick.AddListener(QuestTracker.Instance.CompleteQuestIfRequirementsMet);
    }

    public void SetCurrentDialogueLine(string line)
    {
        currentLine.text = line;
    }

    public void HideDreamerAndDialogue()
    {
        dreamerSprite.gameObject.SetActive(false);
        interactButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ShowDreamer()
    {
        dreamerSprite.gameObject.SetActive(true);
        interactButton.gameObject.SetActive(true);
    }

}
