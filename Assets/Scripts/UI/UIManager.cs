using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject interactUI;
    public GameObject questUI;
    public GameObject witchHutUI;
    public GameObject dialogueUI;
    public GameObject flaskUI;
    public GameObject cutsceneUI;

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

    public void SetUIForPreset(UIPreset preset)
    {
        if (preset == UIPreset.Garden)
        {
            SetActiveIfNotNull(interactUI, true);
            SetActiveIfNotNull(questUI, true);
            SetActiveIfNotNull(witchHutUI, false);
            SetActiveIfNotNull(dialogueUI, false);
            SetActiveIfNotNull(flaskUI, true);
            SetActiveIfNotNull(cutsceneUI, false);

            PlayerController.Instance.EnablePlayerControl();
            if(DialogueUI.Instance!= null) DialogueUI.Instance.ShowDreamer(); //Garden should always reset dreamer for re-entry
        }
        else if (preset == UIPreset.WitchHut)
        {
            SetActiveIfNotNull(interactUI, false);
            SetActiveIfNotNull(questUI, false);
            SetActiveIfNotNull(witchHutUI, true);
            SetActiveIfNotNull(dialogueUI, false);
            SetActiveIfNotNull(cutsceneUI, false);

            PlayerController.Instance.DisablePlayerControl();
        } else if (preset == UIPreset.Cutscene)
        {
            SetActiveIfNotNull(interactUI, false);
            SetActiveIfNotNull(questUI, false);
            SetActiveIfNotNull(witchHutUI, false);
            SetActiveIfNotNull(dialogueUI, false);
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
