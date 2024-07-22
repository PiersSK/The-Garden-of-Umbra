using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent;}
    [SerializeField] private string currentState;
    [SerializeField] public bool isSkittish = false;
    public Vector3 spawnPoint;
    public Path path;
    private GameObject player;
    public GameObject Player { get => player;}
    private PlayerController playerController;
    public PlayerController PlayerController { get => playerController;}
    public float sightDistance = 5f;
    private SpawnManager spawnManagerRef;
    

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = PlayerController.FindAnyObjectByType<PlayerController>();
        spawnPoint = path.waypoints[0].position;
        spawnManagerRef = GameObject.FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CanDetectPlayer();
        currentState = stateMachine.activeState.ToString();
        if (currentState == "DespawnedState" && Vector3.Distance(spawnPoint, Player.transform.position) < sightDistance)
        {
            RespawnCreature();
        }
    }

    public bool CanDetectPlayer()
    {
        if(player != null)
        {
            //is the player close enough to be detected?
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                return true;
            }
        }
        return false;
    }

    public void DespawnCreature()
    {
        gameObject.SetActive(false);
    }

    public void RespawnCreature()
    {
        spawnManagerRef.SpawnCreature();
        Destroy(gameObject);
    }
}
