using UnityEngine;

public class ShadowInteractable : Interactable
{
    public Creatures creature;
    public SpawnManager spawnManager;

    public AspectUI aspectUI;

    private void Start()
    {
        if(aspectUI != null) aspectUI.UpdateAspectUI(creature.shadow);
        promptText = "Take Shadow [" + creature.shadow.size.ToString() + "]";
    }

    private void Update()
    {
        if(aspectUI != null) aspectUI.gameObject.SetActive(PlayerInteract.Instance.interactablesInRange.Contains(this));
    }

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        var shadowAdded = InventoryManager.Instance.AddShadow(creature.shadow, GetComponent<SpriteRenderer>().sprite);

        QuestTracker.Instance.headAspect = creature.shadow.headAspect;
        QuestTracker.Instance.bodyAspect = creature.shadow.bodyAspect;
        QuestTracker.Instance.feetAspect = creature.shadow.feetAspect;
    }

    public override bool CanInteract()
    {
        return GetComponent<SpriteRenderer>().shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off 
            && InventoryManager.Instance.IsThereSpaceForAShadowSir(creature.shadow);
    }
}
