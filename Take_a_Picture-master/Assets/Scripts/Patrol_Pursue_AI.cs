using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol_Pursue_AI : MonoBehaviour
{
    enum EnemyState { patrolling, pursuing };
    public static Waypoint[] Waypoints;
    private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl AI;
    private EnemyState enemyState = EnemyState.patrolling;
    public float patrolSpeed = 0.75f;
    public float pursueSpeed = 1f;
    private static Transform playerTransform;
    public float captureRadius = 2f;

    void Awake()
    {
        Waypoints = FindObjectsOfType<Waypoint>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        AI = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
    }

    
    private Transform GetNearestWaypoint(){
        Transform nearestWaypoint = Waypoints[0].transform;
        foreach (Waypoint w in Waypoints)
        {
            if ((w.transform.position - transform.position).sqrMagnitude <
                (nearestWaypoint.position - transform.position).sqrMagnitude)
            {
                nearestWaypoint = w.transform;
            }
        }
        return nearestWaypoint;
    }


    void Start()
    {
        FindNextWaypoint();
    }


    void Update()
    {
        if ((transform.position - playerTransform.position).magnitude < captureRadius)
        {
            SceneManager.LoadScene("03b Lose Screen");
        }

        switch (enemyState)
        {
            case EnemyState.patrolling:
                Patrol();
                break;
            case EnemyState.pursuing:
                Pursue();
                break;
        }
    }

    private void Pursue()
    {
        AI.agent.speed = pursueSpeed;
        //TODO: Check for lost sight of player [else if Patrol]
        throw new NotImplementedException();
    }

    private void Patrol()
    {
        AI.agent.speed = patrolSpeed;
        //TODO: Check if player is in sight 
    }


    private void FindNextWaypoint()//Todo: fix this. It currently just grabs one at random 
    {
        AI.target = Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].transform;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Waypoint")
        {
            FindNextWaypoint();
        }
    }

}
