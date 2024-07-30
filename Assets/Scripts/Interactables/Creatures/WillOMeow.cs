using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillOMeow : ShadowInteractable
{
    public GameObject surpriseMarker;
    public GameObject path;

    protected override void Start()
    {
        base.Start();
    }

    public override bool CanInteract()
    {
        return base.CanInteract() && !GetComponent<Animator>().GetBool("IsRunning");
    }
}
