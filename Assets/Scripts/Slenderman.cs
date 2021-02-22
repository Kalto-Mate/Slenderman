using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slenderman : MonoBehaviour
{
    // Start is called before the first frame update
    // Altura 8.75, radio 1

    public SphereCollider Range;
    public SpriteRenderer sprite;

    //----------------------------------------------------------------------------------
    NavMeshAgent navMeshAgent;
        float walkSpeedAcceleration = 8;        float walkSpeed = 3;
        float warpSpeedAcceleration = 10000000;    float warpSpeed = 10000000;
    public enum Mode { ChasePlayer, Idle, ChaseFront};
    Mode currentMode;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = walkSpeed;
        navMeshAgent.acceleration = walkSpeedAcceleration;
    }

    void Start()
    {
        currentMode = Mode.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMode != Mode.Idle) 
        {
            if (currentMode == Mode.ChasePlayer)        ChasePlayer();
            else if (currentMode == Mode.ChaseFront)    ChaseFront();
        }
        
    }
    //RUTINAS----------------------------------------------------------------------------------------
    public void ChasePlayer()
    {
        navMeshAgent.SetDestination(GameManager.singleInstance.Jugador.transform.position);
    }
    public void ChaseFront()
    {
        navMeshAgent.SetDestination(GameManager.singleInstance.frontSpawn.transform.position);
    }

    //ACTIONS----------------------------------------------------------------------------------------
    public void Banish(bool input)
    {
        Range.enabled = !input;
        sprite.enabled = !input;
        currentMode = Mode.Idle;
    }

    public bool AppearInFront()
    {
        Vector3 DestinationRaw = GameManager.singleInstance.Jugador.transform.position;
        float y = Terrain.activeTerrain.SampleHeight(DestinationRaw) + Terrain.activeTerrain.transform.position.y;
        Vector3 Destination = new Vector3(DestinationRaw.x, y, DestinationRaw.z);

        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(Destination, out navMeshHit, 1f, NavMesh.AllAreas))
        {
            StartCoroutine(Coroutine_AppearInFront()); return true;
        }
        else
        {
            print("Invalid destination, aborting AppearInFront");
            return true;
        }
            
        
            
    }

    IEnumerator Coroutine_AppearInFront()
    {
        Banish(true);
        this.transform.position = GameManager.singleInstance.Jugador.transform.position; //Se teletransporta al jugador
        navMeshAgent.SetDestination(GameManager.singleInstance.frontSpawn.transform.position);

        //SuperVelocidad
        navMeshAgent.acceleration = warpSpeedAcceleration;
        navMeshAgent.speed = warpSpeed;
        yield return new WaitForSeconds(.2f);


        //Velocidad normal
        navMeshAgent.SetDestination(this.transform.position);
        navMeshAgent.acceleration = walkSpeedAcceleration;
        navMeshAgent.speed = walkSpeedAcceleration;

        Banish(false);

    }

}
