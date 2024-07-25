using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aspects;

public class ShadowInteractable : Interactable
{

    // TEMP: Hardcoded beans shadow for testing until inventory implementatio
    private ShadowAspect.Aspect feetAspect = ShadowAspect.Aspect.Beans;
    [SerializeField]
    private Shadow shadow;

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        var shadowAdded = InventoryManager.Instance.AddShadow(shadow);
        QuestTracker.Instance.feetAspect = feetAspect;
    }

    public override bool CanInteract()
    {
        return GetComponent<SpriteRenderer>().shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
