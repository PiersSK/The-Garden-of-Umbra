using UnityEngine;
using Aspects;
using UnityEngine.UI;
using UnityEngine.AI;

public class ShadowInteractable : Interactable
{
    public Creatures creature;
    public SpawnManager spawnManager;
    [SerializeField] private GameObject smokeObj;

    public AspectUI aspectUI;

    protected virtual void Start()
    {
        if(aspectUI != null) aspectUI.UpdateAspectUI(creature.shadow);
        spawnManager = FindAnyObjectByType<SpawnManager>();
        promptText = "Take Shadow [" + creature.shadow.size.ToString() + "]";
    }

    private void Update()
    {
        if(aspectUI != null) aspectUI.gameObject.SetActive(PlayerInteract.Instance.interactablesInRange.Contains(this));
    }

    public override void Interact()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        var shadowAdded = InventoryManager.Instance.AddShadow(creature.shadow);

        GameObject obj = Instantiate(smokeObj, transform);
        obj.GetComponent<Animator>().SetTrigger("Puff");
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<StateMachine>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Invoke("DespawnWithoutShadow", 2f);
    }

    private void DespawnWithoutShadow()
    {
        spawnManager.DespawnCreatureWithoutRespawn(creature);
    }

    public override bool CanInteract()
    {
        return GetComponent<SpriteRenderer>().shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off 
            && InventoryManager.Instance.IsThereSpaceForAShadowSir(creature.shadow);
    }
}
