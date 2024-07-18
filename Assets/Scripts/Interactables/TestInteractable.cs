using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    public override void Interact()
    {
        transform.position = new Vector3(
                Random.Range(-20f, 20f),
                transform.position.y,
                Random.Range(-20f, 20f)
            );
    }
}
