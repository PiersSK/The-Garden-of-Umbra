using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTransition : MonoBehaviour
{
    [SerializeField] private UIManager.UIPreset presetToLoad;

    private const string PLAYEROBJECTNAME = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == PLAYEROBJECTNAME)
        {
            UIManager.Instance.SetUIForPreset(presetToLoad);
        }
    }
}
