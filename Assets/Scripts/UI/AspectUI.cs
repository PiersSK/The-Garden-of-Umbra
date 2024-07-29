using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AspectUI : MonoBehaviour
{
    [SerializeField] private Image headAspect;
    [SerializeField] private Image bodyAspect;
    [SerializeField] private Image feetAspect;
    [SerializeField] private TextMeshProUGUI shadowName;

    public void UpdateAspectUI(Shadow shadow)
    {
        headAspect.sprite = shadow.headAspectSprite;
        headAspect.color = shadow.headAspect == Aspects.HeadAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        bodyAspect.sprite = shadow.bodyAspectSprite;
        bodyAspect.color = shadow.bodyAspect == Aspects.BodyAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        feetAspect.sprite = shadow.feetAspectSprite;
        feetAspect.color = shadow.feetAspect == Aspects.FeetAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        shadowName.text = shadow.shadowName;
    }
}
