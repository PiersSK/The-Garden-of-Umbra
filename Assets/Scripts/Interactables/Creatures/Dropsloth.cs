using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Dropsloth : ShadowInteractable
{
    public bool hasDropped = false;

    public override bool CanInteract()
    {
        return hasDropped && base.CanInteract();
    }
}
