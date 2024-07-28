using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HedgedogFleeState : BaseState
{
    private float despawnTimer = 0f;
    private float timeToDespawn = 1.5f;

    private Vector3 fleeNorth = new(31.2f, 0f, -3.6f);
    private Vector3 fleeSouth = new(7.6f, 0f, -20.6f);

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
        creatureAgent.GetComponent<Animator>().SetBool("IsRunning", true);
        initialSpeed = creatureAgent.speed * 2f;
        creatureAgent.speed = player.GetComponent<PlayerController>().speed;
        //creatureAgent.stoppingDistance = 0f;
        

    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        creatureAgent.SetDestination(GetBestFleeSpot());

        if (creatureAgent.velocity.x > 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = false;
        else if (creatureAgent.velocity.x < 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = true;

        float percToDespawn = despawnTimer / timeToDespawn;
        creatureAgent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - percToDespawn);

        despawnTimer += Time.deltaTime;
        if (despawnTimer >= timeToDespawn)
        {
            creatureAgent.speed = initialSpeed;
            spawnManager.DespawnCreature(creature);
        }
    }

    private Vector3 GetBestFleeSpot()
    {
        float northDistance = Vector3.Distance(player.transform.position, fleeNorth);
        float southDistance = Vector3.Distance(player.transform.position, fleeSouth);

        if (Mathf.Max(northDistance, southDistance) == northDistance) return fleeNorth;
        else return fleeSouth;
    }
}
