using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgedog : ShadowInteractable
{
    public override bool CanInteract()
    {
        return base.CanInteract() && !GetComponent<Animator>().GetBool("IsRunning");
    }
}
