using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerThoughts : MonoBehaviour
{
    public static PlayerThoughts Instance { get; private set; }

    [SerializeField] private GameObject thought;
    [SerializeField] private TextMeshProUGUI thoughtText;

    private float timeTillFade = 4f;
    private float fadeTimer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.T))
        {
            ShowThought("This is a 5 second test thought", 5f);
        }

        if (thought.activeSelf)
        {
            fadeTimer += Time.deltaTime;
            if(fadeTimer > timeTillFade )
            {
                thought.SetActive(false);
            }
        }
    }

    public void ShowThought(string text, float timeToDisplay)
    {
        thoughtText.text = text;
        timeTillFade = timeToDisplay;
        fadeTimer = 0f;
        thought.SetActive(true);
    }
}
