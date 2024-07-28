using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Scriptable Object/Creature")]
public class Creatures : ScriptableObject
{
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent;}
    public GameObject prefab;
    [SerializeField] private string currentState;
    private GameObject player;
    public GameObject Player { get => player;}
    public bool isSkittish;
    public bool isFriendly;
    public bool isIndifferent;
    public bool isHostile;
    public string pathName;

    public Vector3 spawnPoint;

    public float sightDistance = 5f;

    public enum Creature
    {
        CottonSprite,
        Flittlet,
        LesserSpottedHedgehog,
        Dropsloth,
        Aurafox,
        Dragonpuppy,
        WillOMeow,
        GreaOakWyrm,
        Jetback,
        Primaloggon,
        CaveCow,
        Prismole
    }

    public enum DefaultBehaviour
    {
        Friendly,
        Indifferent,
        Skittish,
        Hostile
    }

    public Creature creature;
    public DefaultBehaviour defaultBehaviour;
    public string defaultState;
    public Shadow shadow;
}
