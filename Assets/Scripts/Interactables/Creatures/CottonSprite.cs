using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSprite : ShadowInteractable
{
    public GameObject intruigeMarker;

    protected override void Start()
    {
        base.Start();
        intruigeMarker.SetActive(false);
    }
    public override bool CanInteract()
    {
        return base.CanInteract() && !GetComponent<Animator>().GetBool("IsSitting");
    }
}
