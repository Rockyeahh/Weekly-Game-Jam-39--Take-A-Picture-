using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol_Pursue_AI : MonoBehaviour
{
    enum EnemyState { patrolling, pursuing };

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

    void Awake()
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
    void Update()
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
        AI.agent.speed = pursueSpeed;
        //TODO: Check for capture [if in captureRadius AI.agent.speed = pursueSpeed;]
        //  if (playerTransform.position.magnitude < captureRadius){
        //       return
        //   } Ignore the check for capture stuff as Dennis has done it but didn't remove the note.
        //TODO: Check for lost sight of player [else if Patrol]
        throw new NotImplementedException();
    }

    private void Patrol()
    {
        AI.agent.speed = patrolSpeed;
        //TODO: Check if player is in sight [if player in captureRadius then Pursue]
        if ((transform.position - AI.target.position).magnitude < waypointDistance)
        {
            FindNextWaypoint(); // Why not just call the private GetNearestWaypoint() method?
        }
    }

    private void FindNextWaypoint()//Todo: fix this. It currently just grabs one at random 
    {
        // GameObject[] Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        // float minDistance = Mathf.Infinity;
        //  Transform closest;

        //   if (Waypoints.Length == 0)
        //    return null;

        //  closest = Waypoints[0].transform;
        // for (int i = 1; i < Waypoints.Length; ++i)
        // {
        //   float distance = (Waypoints[i].transform.position - transform.position).sqrMagnitude;

        //  if (distance < minDistance)
        //  {
        //     closest = Waypoints[i].transform;
        //       minDistance = distance;
        //  }

        //AI.target = Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].transform;
        //AI.target = nearestWaypoint.transform;
        AI.target = Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].transform; // This was originally here.
                                                                                        //  }
    }
}
