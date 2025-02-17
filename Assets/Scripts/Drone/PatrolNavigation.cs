using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PatrolNavigation : MonoBehaviour
{
   
    NavMeshAgent agent;
    float distance;
    Rigidbody2D rb;
    quaternion rotation;
    bullet bulletLink;
    float playerDistance;
    LayerMask mask;
    GameObject instanceBullet;
    bool firing = false;
    
    public Vector3 facingDirection;
    public Vector2 towardsTarget;
    public int targetNumber;
    public bool playerSensed;
    public GameObject bullet;
    public GameObject shell;

    [SerializeField] RaycastEnemy rayCast;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform[] targets;
    [SerializeField] Transform currentTarget;
    [SerializeField] Transform player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip gunShot;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Patrol();
        targetNumber = 0;
        currentTarget = targets[0];
        rayCast = GetComponent<RaycastEnemy>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(transform.position, player.position);

        if(rayCast.TargetHit){
            playerSensed = true;
            currentTarget = player;
            agent.stoppingDistance = 2;
            if (!firing)
            {
                firing = true;
                StartCoroutine(shootReload());
            }

        }
         if ((agent.velocity.magnitude) == 0)
        { 
            towardsTarget = new Vector2(currentTarget.position.x, currentTarget.position.y) - rb.position;
            facingDirection = rayCast.rayDirection;
            float angle = (Mathf.Atan2(towardsTarget.y, towardsTarget.x) - (Mathf.PI / 2)) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }

        agent.SetDestination(currentTarget.position);
        Turn();
        Patrol();
      
            
        
    }

    public void Patrol()
    {
        if(Vector2.Distance(transform.position, currentTarget.position) <= 1 && currentTarget != player) {
            targetNumber++;
            if(targetNumber == targets.Length)
                targetNumber = 0;
            currentTarget = targets[targetNumber];
        }
    }

    public void Turn()
    {
        if(agent.velocity.magnitude > 0)
        {
        facingDirection = agent.velocity;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, facingDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        //Debug.Log(toRotation.ToString());
        }
        
    }

    public IEnumerator shootReload()
    {
        instanceBullet = Instantiate(bullet, transform.position, transform.rotation);
        audioSource.PlayOneShot(gunShot);
        bulletLink = instanceBullet.GetComponent<bullet>();
        if(agent.velocity.magnitude == 0)
        {
        bulletLink.forceX = towardsTarget.normalized.x;
        bulletLink.forceY = towardsTarget.normalized.y;
        }
        else
        {
            bulletLink.forceX = agent.velocity.normalized.x;
            bulletLink.forceY = agent.velocity.normalized.y;
        }
        Instantiate(shell, transform.position, transform.rotation);
        yield return new WaitForSecondsRealtime(1);
        if(firing)
        StartCoroutine(shootReload());
    }
}
