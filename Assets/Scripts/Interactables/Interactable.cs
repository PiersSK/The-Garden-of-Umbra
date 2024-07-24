using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Creatures creature;
    public SpawnManager spawnManager;
    
    public string promptText;
    public int priority = 0;

    void Start()
    {
        CreatureReference creatureReference = GetComponent<CreatureReference>();
        spawnManager = FindObjectOfType<SpawnManager>();
        if (creatureReference != null)
        {
            creature = creatureReference.creature;
        }
    }
    public virtual bool CanInteract()
    {
        return true;
    }

    public virtual void Interact()
    {
        if(spawnManager != null && gameObject != null)
        {
            Debug.Log(gameObject.name + " interacted with!");
            spawnManager.DespawnCreature(creature);
        }
    }
}