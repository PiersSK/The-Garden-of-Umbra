using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public Creatures[] creatures;
    public float distanceFromSpawn = 10f;
    public float respawnDelay = 10f;
    private Dictionary<Creatures, GameObject> activeCreatures = new Dictionary<Creatures, GameObject>();
    public GameObject player;

    private void Awake()
    {
        Instance = this;
    }

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
        newCreature.name = creature.name;

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

        SpriteRenderer spriteRenderer = newCreature.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = newCreature.AddComponent<SpriteRenderer>();
        }

        // TODO: Only dynamically add the relevant states. I think because this is being created statically, any properties can just be privately
        //       maintained in the states themselves rather than passed into the constructors

        //Cotton Sprite
        stateMachine.AddState("CottonSpriteWanderState", new CottonSpriteWanderState(stateMachine, creatureAgent, creature, 10f, player));
        stateMachine.AddState("CottonSpriteSittingState", new CottonSpriteSittingState(stateMachine, creatureAgent, creature, player));
        stateMachine.AddState("CottonSpriteFollowState", new CottonSpriteFollowState(stateMachine, creatureAgent, creature, player));

        //Flittlet
        stateMachine.AddState("FlittletWanderState", new FlittletWanderState(stateMachine, creatureAgent, creature, 10f));

        //Hedgedog
        stateMachine.AddState("HedgedogSleepingState", new HedgedogSleepingState(stateMachine, creatureAgent, creature, player));
        stateMachine.AddState("HedgedogFleeState", new HedgedogFleeState(stateMachine, creatureAgent, player, creature, this));

        //Dragonpuppy
        stateMachine.AddState("DragonpuppyDashState", new DragonpuppyDashState(stateMachine, creatureAgent, creature, 10f, player));
        stateMachine.AddState("DragonpuppyPlayerState", new DragonpuppyPlayerState(stateMachine, creatureAgent, creature, player));

        //Aurafox
        stateMachine.AddState("AurafoxSleepingState", new AurafoxSleepingState(stateMachine, creatureAgent, creature, player));
        stateMachine.AddState("AurafoxAwakeState", new AurafoxAwakeState(stateMachine, creatureAgent, creature, player));
        stateMachine.AddState("AurafoxTeleportState", new AurafoxTeleportState(stateMachine, creatureAgent, creature, player));

        //Great Oak Wyrm
        stateMachine.AddState("GreatOakWyrmIdleState", new GreatOakWyrmIdleState(stateMachine, creatureAgent, creature, player, spriteRenderer));
        stateMachine.AddState("GreatOakWyrmFollowState", new GreatOakWyrmFollowState(stateMachine, creatureAgent, creature, player, spriteRenderer));

        //Prismole
        stateMachine.AddState("PrismoleFleeState", new PrismoleFleeState(stateMachine, creatureAgent, creature, player));
        stateMachine.AddState("PrismoleIdleState", new PrismoleIdleState(stateMachine, creatureAgent, creature, player));


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

    public void SpawnCreatureFromShadow(Shadow shadow)
    {
        foreach (Creatures creature in creatures)
        {
            if(creature.shadow == shadow)
            {
                SpawnCreature(creature);
                break;
            }
        }
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

    public void DespawnCreatureWithoutRespawn(Creatures creature)
    {
        if (activeCreatures.TryGetValue(creature, out GameObject newCreature))
        {
            Destroy(newCreature);
            activeCreatures.Remove(creature);
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