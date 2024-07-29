using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerThoughts : MonoBehaviour
{
    public static PlayerThoughts Instance { get; private set; }

    public GameObject thought;
    public GameObject reaction;
    [SerializeField] private TextMeshProUGUI thoughtText;

    private float timeTillFade = 4f;
    private float fadeTimer = 0f;

    private float reactionMaxTime = 2f;
    private float reactionTimer = 0f;

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
            if(fadeTimer > timeTillFade)
            {
                thought.SetActive(false);
            }
        }

        if (reaction.activeSelf)
        {
            reactionTimer += Time.deltaTime;
            if(reactionTimer > reactionMaxTime)
            {
                reaction.SetActive(false);
            }
        }
    }

    public void ShowReaction(float timeToDisplay)
    {
        reactionMaxTime = timeToDisplay;
        reactionTimer = 0f;
        reaction.SetActive(true);
    }

    public void ShowThought(string text, float timeToDisplay)
    {
        thoughtText.text = text;
        timeTillFade = timeToDisplay;
        fadeTimer = 0f;
        thought.SetActive(true);
    }
}
