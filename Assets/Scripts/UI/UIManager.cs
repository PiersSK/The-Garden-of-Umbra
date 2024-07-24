using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject InteractUI;
    public GameObject QuestUI;
    public GameObject WitchHutUI;
    public GameObject DialogueUI;

    public enum UIPreset
    {
        Garden,
        WitchHut
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

    public void SetUIForPreset(UIPreset preset)
    {
        if (preset == UIPreset.Garden)
        {
            SetActiveIfNotNull(InteractUI, true);
            SetActiveIfNotNull(QuestUI, true);
            SetActiveIfNotNull(WitchHutUI, false);

            PlayerController.Instance.EnablePlayerControl();
        }
        else if (preset == UIPreset.WitchHut)
        {
            SetActiveIfNotNull(InteractUI, false);
            SetActiveIfNotNull(QuestUI, false);
            SetActiveIfNotNull(WitchHutUI, true);

            PlayerController.Instance.DisablePlayerControl();
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
