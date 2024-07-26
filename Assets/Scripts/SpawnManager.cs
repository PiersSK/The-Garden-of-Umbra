using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public Creatures[] creatures;
    public float distanceFromSpawn = 10f;
    public float respawnDelay = 10f;
    private Dictionary<Creatures, GameObject> activeCreatures = new Dictionary<Creatures, GameObject>();
    public GameObject player;
    void Start()
    {
        SpawnCreatures();
    }

    void Update()
    {

    }

    void SpawnCreature(Creatures creature)
    {
        GameObject newCreature = Instantiate(creature.prefab, creature.spawnPoint, Quaternion.identity);

        CreatureReference creatureReference = newCreature.GetComponent<CreatureReference>();
        if (creatureReference == null)
        {
            creatureReference = newCreature.AddComponent<CreatureReference>();
        }
        creatureReference.creature = creature;

        NavMeshAgent creatureAgent = newCreature.GetComponent<NavMeshAgent>();

        StateMachine stateMachine = newCreature.GetComponent<StateMachine>();
        if (stateMachine == null)
        {
            stateMachine = newCreature.AddComponent<StateMachine>();
        }
        stateMachine.AddState("CottonSpriteWanderState", new CottonSpriteWanderState(stateMachine, creatureAgent, creature, 5f, player));
        stateMachine.AddState("CottonSpriteSittingState", new CottonSpriteSittingState(stateMachine, creatureAgent, creature, player));
        stateMachine.AddState("CottonSpriteFollowState", new CottonSpriteFollowState(stateMachine, creatureAgent, creature, player));

        stateMachine.SetDefaultState(creature.defaultState);
        

        GameObject path = GameObject.Find(creature.pathName);

        Path creaturePath = newCreature.GetComponent<Path>();
        if (creaturePath == null)
        {
            creaturePath = newCreature.AddComponent<Path>();
        }
        creaturePath.path = path;

        activeCreatures[creature] = newCreature;
    }

    void SpawnCreatures()
    {
        foreach (Creatures creature in creatures)
        {
            SpawnCreature(creature);
        }
    }
    
    public void DespawnCreature(Creatures creature)
    {
        if (activeCreatures.TryGetValue(creature, out GameObject newCreature))
        {
            Destroy(newCreature);
            activeCreatures.Remove(creature);
            StartCoroutine(RespawnCreature(creature, respawnDelay, distanceFromSpawn));
        }
    }

    private IEnumerator RespawnCreature(Creatures creature, float delay, float distance)
    {
        yield return new WaitForSeconds(delay);
        if(creature != null)
        {
            if(Vector3.Distance(player.transform.position, creature.spawnPoint) > distance)
            {
                SpawnCreature(creature);
            }
        }
    }

}