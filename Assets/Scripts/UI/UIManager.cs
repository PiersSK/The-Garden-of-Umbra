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


        SceneManager.activeSceneChanged += SetUIForScene;
    }

    private void SetUIForScene(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "Garden")
        {
            SetActiveIfNotNull(InteractUI, true);
            SetActiveIfNotNull(QuestUI, true);
            SetActiveIfNotNull(WitchHutUI, false);
        }
        else if (newScene.name == "WitchHut")
        {
            SetActiveIfNotNull(InteractUI, false);
            SetActiveIfNotNull(QuestUI, false);
            SetActiveIfNotNull(WitchHutUI, true);
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
