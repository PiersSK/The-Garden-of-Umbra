using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Aspects;

public class ChecklistUI : MonoBehaviour
{
    public static ChecklistUI Instance { get; private set; }

    [SerializeField] private List<ChecklistButton> checkButtons = new List<ChecklistButton>(9);

    [SerializeField] private TextMeshProUGUI potionName;

    // Potion name lookups
    private readonly List<string> prefixes = new List<string>() { string.Empty, "Crystal-clear", "Hearty", "Swirling" };
    private readonly List<string> adjectives = new List<string>() { string.Empty, "Protective ", "Pure ", "Inspiring " };
    private readonly List<string> nouns = new List<string>() { string.Empty, "Protection", "Purity", "Inspiration" };
    private readonly List<string> suffixes = new List<string>() { string.Empty, "Embers", "Aura", "Hooves" };


    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        potionName.text = GetPotionName();
    }

    public void UntickHeadButton(HeadAspect aspect)
    {
        foreach(var button in checkButtons)
        {
            if (button.isChecked && button.headAspect == aspect)
                button.ToggleState();
        }
    }

    public void UntickBodyButton(BodyAspect aspect)
    {
        foreach (var button in checkButtons)
        {
            if (button.isChecked && button.bodyAspect == aspect)
                button.ToggleState();
        }
    }

    public void UntickFeetButton(FeetAspect aspect)
    {
        foreach (var button in checkButtons)
        {
            if (button.isChecked && button.feetAspect == aspect)
                button.ToggleState();
        }
    }

    public void UncheckAll()
    {
        foreach (ChecklistButton button in checkButtons)
        {
            if(button.isChecked)
                button.ToggleState();
        }
    }

    public string GetPotionName()
    {
        string prefix = prefixes[(int)QuestTracker.Instance.headToGather];
        string adjective = string.Empty;
        string noun = string.Empty;
        string suffix = suffixes[(int)QuestTracker.Instance.feetToGather];

        if (QuestTracker.Instance.feetToGather != FeetAspect.None)
            adjective = adjectives[(int)QuestTracker.Instance.bodyToGather];
        else
            noun = nouns[(int)QuestTracker.Instance.bodyToGather];

        string potionName = prefix;
        potionName += prefix == string.Empty ? "Potion" : " potion";
        potionName += (adjective + noun + suffix) == string.Empty ? "" : " of ";
        potionName += adjective + noun + suffix;

        return potionName;
    }
}
