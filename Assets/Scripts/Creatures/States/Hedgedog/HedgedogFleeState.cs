using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HedgedogFleeState : BaseState
{
    private float despawnTimer = 0f;
    private float timeToDespawn = 1.5f;

    private float initialSpeed;

    private NavMeshAgent creatureAgent;
    private GameObject player;
    private Creatures creature;
    private SpawnManager spawnManager;

    public HedgedogFleeState(StateMachine stateMachine,
        NavMeshAgent creatureAgent,
        GameObject player,
        Creatures creature,
        SpawnManager spawnManager
        ) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.player = player;
        this.creature = creature;
        this.spawnManager = spawnManager;
    }

    public override void Enter()
    {
        Debug.Log("Entered Flee");
        initialSpeed = creatureAgent.speed;
        creatureAgent.speed = player.GetComponent<PlayerController>().speed;
        //creatureAgent.stoppingDistance = 0f;
        

    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        Vector3 dirFromPlayer = player.transform.position - creatureAgent.transform.position;
        Vector3 target = creatureAgent.transform.position - dirFromPlayer.normalized * 10f;
        creatureAgent.SetDestination(target);

        float percToDespawn = despawnTimer / timeToDespawn;
        creatureAgent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - percToDespawn);

        despawnTimer += Time.deltaTime;
        if (despawnTimer >= timeToDespawn)
        {
            creatureAgent.speed = initialSpeed;
            spawnManager.DespawnCreature(creature);
        }
    }
}
