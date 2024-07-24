using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowInteractable : Interactable
{

    // TEMP: Hardcoded beans shadow for testing until inventory implementatio
    public ShadowAspect.Aspect headAspect = ShadowAspect.Aspect.None;
    public ShadowAspect.Aspect bodyAspect = ShadowAspect.Aspect.None;
    public ShadowAspect.Aspect feetAspect = ShadowAspect.Aspect.None;

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


        QuestTracker.Instance.headAspect = headAspect;
        QuestTracker.Instance.bodyAspect = bodyAspect;
        QuestTracker.Instance.feetAspect = feetAspect;
    }

    public override bool CanInteract()
    {
        return GetComponent<SpriteRenderer>().shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
