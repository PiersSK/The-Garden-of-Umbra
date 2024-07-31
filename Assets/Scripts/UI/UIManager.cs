using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject interactUI;
    public GameObject questUI;
    public GameObject notesUI;
    public GameObject flaskUI;
    public GameObject cutsceneUI;

    public TextMeshProUGUI crouchIndicator;


    public enum UIPreset
    {
        Garden,
        WitchHut,
        Cutscene
    }

    private void Awake()
    {
        if (UIManager.Instance == null)
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
        SetUIForPreset(UIPreset.Garden);
    }

    private void Update()
    {
        SetCrouchIndicator(PlayerController.Instance.isCrouching);
    }

    public void SetCrouchIndicator(bool crouching)
    {
        crouchIndicator.text = crouching ? "Sneaking..." : "";
    }

    public void SetUIForPreset(UIPreset preset)
    {
        if (preset == UIPreset.Garden)
        {
            SetActiveIfNotNull(interactUI, true);
            SetActiveIfNotNull(questUI, true);
            SetActiveIfNotNull(notesUI, true);
            SetActiveIfNotNull(flaskUI, true);
            SetActiveIfNotNull(cutsceneUI, false);

            PlayerController.Instance.EnablePlayerControl();
            if(DialogueUI.Instance!= null) DialogueUI.Instance.ShowDreamer(); //Garden should always reset dreamer for re-entry
        }
        else if (preset == UIPreset.Cutscene)
        {
            SetActiveIfNotNull(interactUI, false);
            SetActiveIfNotNull(questUI, false);
            SetActiveIfNotNull(notesUI, false);
            SetActiveIfNotNull(flaskUI, false);
            SetActiveIfNotNull(cutsceneUI, true);
        }
    }

    private void SetActiveIfNotNull(GameObject obj, bool active)
    {
        if(obj != null)
        {
            obj.SetActive(active);
        }
    }
}
