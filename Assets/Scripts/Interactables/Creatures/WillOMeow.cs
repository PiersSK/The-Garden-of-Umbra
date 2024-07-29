using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillOMeow : ShadowInteractable
{
    public GameObject surpriseMarker;

    protected override void Start()
    {
        base.Start();
        surpriseMarker.SetActive(false);
    }

    public override bool CanInteract()
    {
        return base.CanInteract() && !GetComponent<Animator>().GetBool("IsRunning");
    }
}
