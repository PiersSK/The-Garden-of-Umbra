using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowInteractable : Interactable
{

    // TEMP: Hardcoded beans shadow for testing until inventory implementatio
    private ShadowAspect.Aspect feetAspect = ShadowAspect.Aspect.Beans;

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        QuestTracker.Instance.feetAspect = feetAspect;
    }

    public override bool CanInteract()
    {
        return GetComponent<SpriteRenderer>().shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
