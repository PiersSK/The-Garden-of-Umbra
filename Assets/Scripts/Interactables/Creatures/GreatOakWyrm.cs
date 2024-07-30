
using UnityEngine;

public class GreatOakWyrm : ShadowInteractable
{
    public bool isUnderTree;
    public GameObject surpriseMarker;

    protected override void Start()
    {
        base.Start();
        surpriseMarker.SetActive(false);
    }

    public override bool CanInteract()
    {
        return base.CanInteract() && !isUnderTree;
    }



}