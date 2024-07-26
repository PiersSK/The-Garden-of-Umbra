using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGardenGate : Interactable
{
    public override void Interact()
    {
        gameObject.SetActive(false);
    }
}
