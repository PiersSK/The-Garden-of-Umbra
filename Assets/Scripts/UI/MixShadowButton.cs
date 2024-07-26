using Aspects;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MixShadowButton : MonoBehaviour
{

    public Image buttonImage;
    public Button mixShadowButton;

    public void Awake()
    {
        Button btn = mixShadowButton.GetComponent<Button>();
        btn.onClick.AddListener(MixShadowsOnClick);
    }

    private void MixShadowsOnClick()
    {
        var mixResult = CombineInventoryShadows();
        Debug.Log($"Result Head:{mixResult.headAspect}, Result Body: {mixResult.bodyAspect}, Result Feet: {mixResult.feetAspect}");
    }

    public Shadow CombineInventoryShadows()
    {
        var finalShadow = new Shadow();
        var flasks = InventoryManager.Instance.flasks;

        for (int i = flasks.Count - 1; i > -1; i--)
        {
            if (flasks[i].flaskUnlocked && flasks[i].shadow is not null)
            {
                var flaskShadow = flasks[i].shadow;
                finalShadow.headAspect = flaskShadow.headAspect ==
                    HeadAspect.None ? finalShadow.headAspect : flaskShadow.headAspect;
                finalShadow.bodyAspect = flaskShadow.bodyAspect ==
                    BodyAspect.None ? finalShadow.bodyAspect : flaskShadow.bodyAspect;
                finalShadow.feetAspect = flaskShadow.feetAspect ==
                    FeetAspect.None ? finalShadow.feetAspect : flaskShadow.feetAspect;
            }
        }

        return finalShadow;
    }
}
