using UnityEngine;

public class Prismole : ShadowInteractable
{
    public bool isInLight = false;
    public GameObject surpriseMarker;

    protected override void Start()
    {
    }

    public override bool CanInteract()
    {
        return base.CanInteract() && isInLight;
    }

    public void OnTeleport()
    {
        GameObject obj = Instantiate(smokeObj, transform);
        obj.GetComponent<Animator>().SetTrigger("Puff");
    }
}