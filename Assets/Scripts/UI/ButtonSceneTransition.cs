using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSceneTransition : MonoBehaviour
{
    [SerializeField] private UIManager.UIPreset presetToLoad;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadUIPreset);
    }

    public void LoadUIPreset()
    {
        Debug.Log("loading");
        UIManager.Instance.SetUIForPreset(presetToLoad);
    }

}
