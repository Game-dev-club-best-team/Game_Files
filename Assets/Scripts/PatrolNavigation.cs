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
    public int targetNumber;
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
        targetNumber = 0;
        currentTarget = targets[0];
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
        Turn();
        Patrol();

    }

    public void Patrol()
    {
        if(Vector2.Distance(transform.position, currentTarget.position) <= 0.01 && currentTarget != player) {
            targetNumber++;
            if(targetNumber == targets.Length)
                targetNumber = 0;
            currentTarget = targets[targetNumber];
        }
    }

    public void Turn()
    {
        facingDirection = agent.velocity;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, facingDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
