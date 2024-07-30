using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragonpuppy : ShadowInteractable
{
    public GameObject curiousMarker;

    protected override void Start()
    {
        curiousMarker.SetActive(false);
        base.Start();
    }
}
