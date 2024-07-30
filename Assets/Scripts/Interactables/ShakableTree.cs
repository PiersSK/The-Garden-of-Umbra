using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakableTree : Interactable
{


    public override void Interact()
    {
        Dropsloth sloth = FindAnyObjectByType<Dropsloth>();
        if (sloth is not null)
        {
            if (!sloth.hasDropped)
            {
                sloth.GetComponent<Animator>().SetTrigger("Fall");
                sloth.hasDropped = true;
            }
            GetComponent<Animator>().SetTrigger("Shake");
        }
    }
}
