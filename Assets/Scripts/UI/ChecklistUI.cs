using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUI : MonoBehaviour
{
    public static ChecklistUI Instance { get; private set; }

    [SerializeField] private List<ChecklistButton> checkButtons = new List<ChecklistButton>(9);

    [SerializeField] private TextMeshProUGUI potionName;

    // Potion name lookups
    private readonly List<string> prefixes = new List<string>() { "Crystal-clear", "Hearty", "Swirling" };
    private readonly List<string> adjectives = new List<string>() { "Protective ", "Pure ", "Inspiring " };
    private readonly List<string> nouns = new List<string>() { "Protection", "Purity", "Inspiration" };
    private readonly List<string> suffixes = new List<string>() { "Embers", "Aura", "Hooves" };


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        potionName.text = GetPotionName();
    }

    public void ClearAspectsOfSlot(ShadowAspect.AspectSlot slot)
    {
        foreach (ChecklistButton button in checkButtons)
        {
            if (button.isChecked && button.shadowAspect.slot == slot) button.ToggleState();
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
        string prefix = string.Empty;
        string adjective = string.Empty;
        string noun = string.Empty;
        string suffix = string.Empty;

        List<ShadowAspect> checkedAspects = QuestTracker.Instance.checkedAspects;

        if (checkedAspects.Count == 0)
            return "???";
        else
        {
            foreach (ShadowAspect shadowAspect in checkedAspects)
            {
                if (shadowAspect.slot == ShadowAspect.AspectSlot.Head)
                    prefix = prefixes[(int)shadowAspect.aspect];
                else if (shadowAspect.slot == ShadowAspect.AspectSlot.Feet)
                    suffix = suffixes[(int)shadowAspect.aspect % 3];
                else if (shadowAspect.slot == ShadowAspect.AspectSlot.Body)
                {
                    if (checkedAspects.Any(o => o.slot == ShadowAspect.AspectSlot.Feet))
                        adjective = adjectives[(int)shadowAspect.aspect % 3];
                    else
                        noun = nouns[(int)shadowAspect.aspect % 3];
                }
            }
        }

        string potionName = prefix;
        potionName += prefix == string.Empty ? "Potion" : " potion";
        potionName += (adjective + noun + suffix) == string.Empty ? "" : " of ";
        potionName += adjective + noun + suffix;

        return potionName;
    }
}
