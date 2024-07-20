using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistButton : MonoBehaviour
{
    private bool isChecked = false;
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
        Color stateColor = isChecked ? checkedColor : uncheckedColor;
        GetComponent<Image>().color = stateColor;
    }
}
