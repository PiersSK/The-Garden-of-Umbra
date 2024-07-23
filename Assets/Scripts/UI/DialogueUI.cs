using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI currentLine;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCurrentDialogueLine(string line)
    {
        currentLine.text = line;
    }
}
