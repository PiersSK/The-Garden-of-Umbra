using Aspects;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        if (FlasksAvShadows())
        {
            combinedShadow = CombineInventoryShadows();
            UpdateAspectUI();
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            aspectInfo.SetActive(true);
        }
    }

    public bool FlasksAvShadows()
    {
        var flasks = InventoryManager.Instance.flasks;
        foreach (Flask flask in flasks)
        {
            if (flask.shadow is not null)
            {
                return true;
            }
        }
        return false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultSize;
        aspectInfo.SetActive(false);
        foreach(InventoryItem item in FlasksUI.Instance.inventoryItems)
        {
            item.aspectInfo.SetActive(false);
        }
    }

    public void UpdateAspectUI()
    {
        headAspect.sprite = combinedShadow.headAspectSprite;
        headAspect.color = combinedShadow.headAspect == HeadAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        bodyAspect.sprite = combinedShadow.bodyAspectSprite;
        bodyAspect.color = combinedShadow.bodyAspect == BodyAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        feetAspect.sprite = combinedShadow.feetAspectSprite;
        feetAspect.color = combinedShadow.feetAspect == FeetAspect.None ? new Color(0, 0, 0, 0.2f) : Color.white;

        UpdateInventoryAspectUI();
    }

    public  void UpdateInventoryAspectUI()
    {
        var invenItems = FlasksUI.Instance.inventoryItems;
        for (int i = invenItems.Count - 1; i > -1; i--)
        {
            if(SmallerFlasksAAspects(i, "Head"))
            {
                invenItems[i].headAspect.color = new Color(0,33,0,0.2f);
            }
            if (SmallerFlasksAAspects(i, "Body"))
            {
                invenItems[i].bodyAspect.color = new Color(0, 33, 0, 0.2f);
            }
            if (SmallerFlasksAAspects(i, "Feet"))
            {
                invenItems[i].feetAspect.color = new Color(0, 33, 0, 0.2f);
            }

            invenItems[i].aspectInfo.SetActive(invenItems[i].item.shadow is not null);
        }
        
    }

    public bool SmallerFlasksAAspects(int index, string aspect)
    {
        if (index <= 0)
        {
            return false;
        }

        var flasks = InventoryManager.Instance.flasks;

        for (int i = index; index <= 0; i--)
        {
            switch (aspect)
            {
                case "Head":
                    if (flasks[i - 1].shadow.headAspect != HeadAspect.None)
                    {
                        return true;
                    }
                    break;
                case "Body":
                    if (flasks[i - 1].shadow.bodyAspect != BodyAspect.None)
                    {
                        return true;
                    }
                    break;
                case "Feet":
                    if (flasks[i - 1].shadow.feetAspect != FeetAspect.None)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
        }

        return false;
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
