using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string promptText;
    public int priority = 0;
    public virtual bool CanInteract()
    {
        return true;
    }

    public virtual void Interact()
    {
        Debug.Log(gameObject.name + " interacted with!");
    }
}