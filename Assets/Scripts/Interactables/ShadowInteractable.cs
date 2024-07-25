using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aspects;

public class ShadowInteractable : Interactable
{
    public Creatures creature;
    public SpawnManager spawnManager;


    public override void Interact()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        var shadowAdded = InventoryManager.Instance.AddShadow(shadow);

        QuestTracker.Instance.headAspect = creature.shadow.headAspect;
        QuestTracker.Instance.bodyAspect = creature.shadow.bodyAspect;
        QuestTracker.Instance.feetAspect = creature.shadow.feetAspect;
    }

    public override bool CanInteract()
    {
        return GetComponent<SpriteRenderer>().shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off 
            && InventoryManager.Instance.IsThereSpaceForAShadowSir(shadow);
    }
}
