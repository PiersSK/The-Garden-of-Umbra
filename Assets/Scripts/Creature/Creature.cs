using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent;}
    [SerializeField] private string currentState;
    [SerializeField] public bool isSkittish = false;
    public Path path;
    private GameObject player;
    public GameObject Player { get => player;}

    private PlayerController playerController;
    public PlayerController PlayerController { get => playerController;}
    public float sightDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = PlayerController.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CanDetectPlayer();
        currentState = stateMachine.activeState.ToString();
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
}
