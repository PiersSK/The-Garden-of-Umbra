using Aspects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MixShadowUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("UI")]
    public Image image;
    public GameObject aspectInfo;

    public Image headAspect;
    public Image bodyAspect;
    public Image feetAspect;

    public Shadow combinedShadow;
    private Vector3 defaultSize;
    public void Awake()
    {
        defaultSize = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var flasksHaveShadow = false;
        var flasks = InventoryManager.Instance.flasks;
        foreach (Flask flask in flasks) {
           if ( flask.shadow is not null )
            {
                flasksHaveShadow = true;
            }
        }
        if (flasksHaveShadow)
        {
            combinedShadow = CombineInventoryShadows();
            UpdateAspectUI();
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            aspectInfo.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultSize;
        aspectInfo.SetActive(false);
    }

    public void UpdateAspectUI()
    {
        headAspect.sprite = combinedShadow.headAspectSprite;
        headAspect.color = combinedShadow.headAspect == HeadAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        bodyAspect.sprite = combinedShadow.bodyAspectSprite;
        bodyAspect.color = combinedShadow.bodyAspect == BodyAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        feetAspect.sprite = combinedShadow.feetAspectSprite;
        feetAspect.color = combinedShadow.feetAspect == FeetAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;
    }

    public Shadow CombineInventoryShadows()
    {
        var combinedShadow = new Shadow();
        var flasks = InventoryManager.Instance.flasks;

        for (int i = 0; i < flasks.Count; i++)
        {
            if (flasks[i].flaskUnlocked && flasks[i].shadow is not null)
            {
                var flaskShadow = flasks[i].shadow;

                if (combinedShadow.headAspect == HeadAspect.None)
                {
                    combinedShadow.headAspect = flaskShadow.headAspect ==
                    HeadAspect.None ? combinedShadow.headAspect : flaskShadow.headAspect;
                    combinedShadow.headAspectSprite = flaskShadow.headAspectSprite is not null ? flaskShadow.headAspectSprite : combinedShadow.headAspectSprite;
                }
                if (combinedShadow.bodyAspect == BodyAspect.None)
                {
                    combinedShadow.bodyAspect = flaskShadow.bodyAspect ==
                    BodyAspect.None ? combinedShadow.bodyAspect : flaskShadow.bodyAspect;
                    combinedShadow.bodyAspectSprite = flaskShadow.bodyAspectSprite is not null ? flaskShadow.bodyAspectSprite : combinedShadow.bodyAspectSprite;
                }
                if (combinedShadow.feetAspect == FeetAspect.None)
                {
                    combinedShadow.feetAspect = flaskShadow.feetAspect ==
                    FeetAspect.None ? combinedShadow.feetAspect : flaskShadow.feetAspect;
                    combinedShadow.feetAspectSprite = flaskShadow.feetAspectSprite is not null ? flaskShadow.feetAspectSprite : combinedShadow.feetAspectSprite;
                }
            }
        }
        return combinedShadow;
    }
}
