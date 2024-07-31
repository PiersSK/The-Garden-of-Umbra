
using UnityEngine;

public class GreatOakWyrm : ShadowInteractable
{
    public bool isUnderTree;
    public GameObject questionMarker;
    protected override void Start()
    {
        base.Start();
    }

    public override bool CanInteract()
    {
        return base.CanInteract() && !isUnderTree;
    }

}