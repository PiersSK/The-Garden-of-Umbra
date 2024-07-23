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
            InteractUI.SetActive(true);
            QuestUI.SetActive(true);
            WitchHutUI.SetActive(false);
        }
        else if (newScene.name == "WitchHut")
        {
            InteractUI.SetActive(false);
            QuestUI.SetActive(false);
            WitchHutUI.SetActive(true);
        }
    }
}
