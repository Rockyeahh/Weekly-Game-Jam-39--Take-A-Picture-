using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol_Pursue_AI : MonoBehaviour
{
    public enum EnemyStates { patrolling, pursuing };
    public static Waypoint[] Waypoints;
    private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl AI;
    public EnemyStates EnemyState
    {
        get { return enemyState; }
        set
        {
            if (enemyState != value)
            {
                enemyState = value;
                if (enemyState == Patrol_Pursue_AI.EnemyStates.patrolling)
                {
                    FindNextWaypoint();
                }
            }
        }
    }
    private EnemyStates enemyState = EnemyStates.patrolling;
    public float patrolSpeed = 0.75f;
    public float pursueSpeed = 1f;
    private static Transform playerTransform;
    public float captureRadius = 2f;
    public float pursueRadius = 10f;
    public static LevelManager levelManager;
    private Transform lastWaypoint;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
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
        if ((transform.position - playerTransform.position).magnitude < pursueRadius)
        {
            if (levelManager.AlarmSounded)
            {
                enemyState = EnemyStates.pursuing;
            }
            if ((transform.position - playerTransform.position).magnitude < captureRadius)
            {
                ScoreKeeper.score = 0;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
                SceneManager.LoadScene("03b Lose Screen");
            }
        }
        else
        {
            enemyState = EnemyStates.patrolling;
        }

        switch (enemyState)
        {
            case EnemyStates.patrolling:
                Patrol();
                break;
            case EnemyStates.pursuing:
                Pursue();
                break;
        }
    }

    private void Pursue()
    {
        AI.agent.speed = pursueSpeed;
        AI.target = playerTransform;

    }

    private void Patrol()
    {
        AI.agent.speed = patrolSpeed;
        //TODO: Check if player is in sight 
    }


    private void FindNextWaypoint()//Todo: fix this. It currently just grabs one at random 
    {
        AI.target = Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].transform;
        lastWaypoint = AI.target;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Waypoint")
        {
            FindNextWaypoint();
        }
    }

}
