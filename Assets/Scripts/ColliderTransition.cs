using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTransition : MonoBehaviour
{
    [SerializeField] private string newSceneName;

    private const string PLAYEROBJECTNAME = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == PLAYEROBJECTNAME)
        {
            SceneManager.LoadScene(newSceneName);
        }
    }
}
