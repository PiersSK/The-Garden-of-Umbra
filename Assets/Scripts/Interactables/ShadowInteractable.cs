using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowInteractable : Interactable
{
    public Creatures creature;
    public SpawnManager spawnManager;

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


        QuestTracker.Instance.headAspect = creature.shadow.headAspect;
        QuestTracker.Instance.bodyAspect = creature.shadow.bodyAspect;
        QuestTracker.Instance.feetAspect = creature.shadow.feetAspect;
    }

    public override bool CanInteract()
    {
        return GetComponent<SpriteRenderer>().shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
