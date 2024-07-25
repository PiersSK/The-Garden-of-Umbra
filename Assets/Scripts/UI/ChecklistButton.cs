using Aspects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistButton : MonoBehaviour
{
    public HeadAspect headAspect;
    public BodyAspect bodyAspect;
    public FeetAspect feetAspect;

    public bool isChecked = false;
    private Image buttonImage;

    private Color uncheckedColor = new Color(255, 255, 255, 0.1f);
    private Color checkedColor = new Color(255, 255, 255, 0.9f);

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = uncheckedColor;

        GetComponent<Button>().onClick.AddListener(ToggleState);
    }

    public void ToggleState()
    {
        isChecked = !isChecked;
        UpdateStateUI();

        if (isChecked) AddToChecklist();
        else RemoveFromChecklist();
    }

    private void AddToChecklist()
    {
        if (headAspect != HeadAspect.None)
        {
            if (QuestTracker.Instance.headToGather != HeadAspect.None)
                ChecklistUI.Instance.UntickHeadButton(QuestTracker.Instance.headToGather);

            QuestTracker.Instance.headToGather = headAspect;
        } else if (bodyAspect != BodyAspect.None)
        {
            if (QuestTracker.Instance.bodyToGather != BodyAspect.None)
                ChecklistUI.Instance.UntickBodyButton(QuestTracker.Instance.bodyToGather);

            QuestTracker.Instance.bodyToGather = bodyAspect;
        } else if (feetAspect != FeetAspect.None)
        {
            if (QuestTracker.Instance.feetToGather != FeetAspect.None)
                ChecklistUI.Instance.UntickFeetButton(QuestTracker.Instance.feetToGather);
            QuestTracker.Instance.feetToGather = feetAspect;
        }
    }

    private void RemoveFromChecklist()
    {
        if (headAspect != HeadAspect.None)
            QuestTracker.Instance.headToGather = HeadAspect.None;
        else if (bodyAspect != BodyAspect.None)
            QuestTracker.Instance.bodyToGather = BodyAspect.None;
        else if (feetAspect != FeetAspect.None)
            QuestTracker.Instance.feetToGather = FeetAspect.None;
    }

    private void UpdateStateUI()
    {
        Color stateColor = isChecked ? checkedColor : uncheckedColor;
        GetComponent<Image>().color = stateColor;
    }

}
