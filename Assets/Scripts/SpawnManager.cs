using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject creature1;
    public UnityEngine.Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnCreature()
    {
        creature1 = Instantiate(creature1, spawnPoint, UnityEngine.Quaternion.identity);
    }
}
