using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUI : MonoBehaviour
{
    public static ChecklistUI Instance { get; private set; }

    [SerializeField] private List<ChecklistButton> checkButtons = new List<ChecklistButton>(9);

    public List<ChecklistButton> checkedAspects = new();

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
                if(!checkedAspects.Contains(button)) checkedAspects.Add(button);
            } else if(checkedAspects.Contains(button)) {
                checkedAspects.Remove(button);
            }
        }
    }

    public bool CanCheckAspect(ShadowAspect.AspectSlot slot)
    {
        foreach(ChecklistButton button in checkedAspects) {
            if (button.aspectSlot == slot) return false;
        }

        return true;
    }
}
