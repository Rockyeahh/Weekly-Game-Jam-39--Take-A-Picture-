using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol_Pursue_AI : MonoBehaviour {
    enum EnemyState {patrolling, pursuing };

    //public Vector3 boundaryMin;
    //public Vector3 boundaryMax;
    public static Waypoint[] Waypoints;
    private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl AI;
    private EnemyState enemyState = EnemyState.patrolling;
    public float waypointDistance = 1f;
    public float patrolSpeed = 0.75f;
    public float pursueSpeed = 1f;
    public static Transform playerTransform;
    public float captureRadius = 2f;

    void Awake ()
    {

        //if (Waypoints.Length == 0)
        {
            Waypoints = FindObjectsOfType<Waypoint>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        AI = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        AI.target = GetNearestWaypoint();
	}

    private Transform GetNearestWaypoint()
    {
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

    // Update is called once per frame
    void Update ()
    {
        if ((transform.position - playerTransform.position).magnitude < captureRadius)
        {
            //end level
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // I swapped this out for loading the lose screen.
            // load lose screen
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
        AI.agent.speed = patrolSpeed;
        //TODO: Check for capture
        //TODO: Check for lost sight of player
        throw new NotImplementedException();
    }

    private void Patrol()
    {
        AI.agent.speed = patrolSpeed;
        //TODO: Check if player is in sight
        if ((transform.position - AI.target.position).magnitude < waypointDistance)
        {
           FindNextWaypoint();
        }
    }

    private void FindNextWaypoint()//Todo: fix this. It currently just grabs one at random 
    {
        AI.target = Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].transform;
    }
}
