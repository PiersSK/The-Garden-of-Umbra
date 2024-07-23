using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static ShadowAspect;

public class ChecklistButton : MonoBehaviour
{
    public ShadowAspect shadowAspect;

    public bool isChecked = false;
    private Image buttonImage;

    private Color uncheckedColor = new Color(255, 255, 255, 0.1f);
    private Color checkedColor = new Color(255, 255, 255, 0.9f);

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = uncheckedColor;

        GetComponent<Button>().onClick.AddListener(ToggleState);

        if (QuestTracker.Instance.checkedAspects.Contains(shadowAspect))
        {
            isChecked = true;
            UpdateStateUI();
        }
    }

    public void ToggleState()
    {
        if (!isChecked) ChecklistUI.Instance.ClearAspectsOfSlot(shadowAspect.slot);

        isChecked = !isChecked;
        UpdateStateUI();

        if (isChecked) QuestTracker.Instance.checkedAspects.Add(shadowAspect);
        else QuestTracker.Instance.checkedAspects.Remove(shadowAspect);
    }

    private void UpdateStateUI()
    {
        Color stateColor = isChecked ? checkedColor : uncheckedColor;
        GetComponent<Image>().color = stateColor;
    }

}
