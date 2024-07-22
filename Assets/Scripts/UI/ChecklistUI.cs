using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUI : MonoBehaviour
{
    public static ChecklistUI Instance { get; private set; }

    [SerializeField] private List<ChecklistButton> checkButtons = new List<ChecklistButton>(9);
    private List<ChecklistButton> checkedAspects = new();

    private bool footAspectChecked = false; // Used for potion names
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
        foreach (ChecklistButton button in checkButtons)
        {
            if (button.isChecked)
            {
                if (!checkedAspects.Contains(button))
                {
                    checkedAspects.Add(button);
                    if(button.aspectSlot == ShadowAspect.AspectSlot.Feet) footAspectChecked = true;
                }
            } else if(checkedAspects.Contains(button)) {
                checkedAspects.Remove(button);
                if (button.aspectSlot == ShadowAspect.AspectSlot.Feet) footAspectChecked = false;
            }
        }

        potionName.text = GetPotionName();
    }

    public void ClearAspectsOfSlot(ShadowAspect.AspectSlot slot)
    {
        foreach(ChecklistButton button in checkedAspects) {
            if (button.aspectSlot == slot) button.ToggleState();
        }
    }

    private string GetPotionName()
    {
        string prefix = string.Empty;
        string adjective = string.Empty;
        string noun = string.Empty;
        string suffix = string.Empty;

        if (checkedAspects.Count == 0)
            return "???";
        else
        {
            foreach(ChecklistButton button in checkedAspects)
            {
                if(button.aspectSlot == ShadowAspect.AspectSlot.Head)
                    prefix = prefixes[(int)button.aspect];
                else if (button.aspectSlot == ShadowAspect.AspectSlot.Feet)
                    suffix = suffixes[(int)button.aspect % 3];    
                else if (button.aspectSlot == ShadowAspect.AspectSlot.Body)
                {
                    if(footAspectChecked)
                        adjective = adjectives[(int)button.aspect % 3];
                    else
                        noun = nouns[(int)button.aspect % 3];
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
