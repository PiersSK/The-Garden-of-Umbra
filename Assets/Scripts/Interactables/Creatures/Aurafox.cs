using UnityEngine;

public class Aurafox : ShadowInteractable
{

    public GameObject surpriseMarker;

    protected override void Start()
    {
        base.Start();
        surpriseMarker.SetActive(false);
    }
    public override bool CanInteract()
    {
        return base.CanInteract() && !GetComponent<Animator>().GetBool("IsAwake");
    }
}
