using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNavigation : MonoBehaviour
{
    [SerializeField] Transform currentTarget;
    NavMeshAgent agent;
    float distance;
    Rigidbody2D rb;
    Vector3 facingDirection;
    quaternion rotation;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform[] targets;
    public bool playerSensed;
    [SerializeField] Transform player;
    float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.position);
        if (playerDistance <= 5)
        {
            playerSensed = true;
            currentTarget = player;
        }
       
        agent.SetDestination(currentTarget.position);
        facingDirection = agent.velocity;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, facingDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        for(int i = 0; i < targets.Length; i++)
        {
            currentTarget = targets[i];
            while(transform.position != currentTarget.position) { 
                agent.SetDestination(currentTarget.position);
            }
            
        }
    }

    public void Patrol()
    {
        
    }
}
