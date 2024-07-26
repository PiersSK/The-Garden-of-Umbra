using Aspects;
using TMPro;
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
    public TextMeshProUGUI combinedShadowName;

    private Shadow combinedShadow;

    private void MixShadowsOnClick()
    {
        var mixResult = CombineInventoryShadows();
        Debug.Log($"Result Head:{mixResult.headAspect}, Result Body: {mixResult.bodyAspect}, Result Feet: {mixResult.feetAspect}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CombineInventoryShadows();
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        aspectInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       aspectInfo.SetActive(false);
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
                    headAspect.sprite = flaskShadow.headAspectSprite is not null ? flaskShadow.headAspectSprite : null;
                }
                if (combinedShadow.bodyAspect == BodyAspect.None)
                {
                    combinedShadow.bodyAspect = flaskShadow.bodyAspect ==
                    BodyAspect.None ? combinedShadow.bodyAspect : flaskShadow.bodyAspect;
                    bodyAspect.sprite = flaskShadow.bodyAspectSprite is not null ? flaskShadow.bodyAspectSprite : null;
                }
                if (combinedShadow.feetAspect == FeetAspect.None)
                {
                    combinedShadow.feetAspect = flaskShadow.feetAspect ==
                    FeetAspect.None ? combinedShadow.feetAspect : flaskShadow.feetAspect;
                    feetAspect.sprite = flaskShadow.feetAspectSprite is not null ? flaskShadow.feetAspectSprite : null;
                }
            }
        }
        return combinedShadow;
    }
}
