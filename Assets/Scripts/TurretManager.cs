using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target;
    public float rotationSpeed;
    public Rigidbody2D rb;
    public bool waiting;
    Vector2 facingDirection;
    Vector2 towardsTarget;

    RaycastEnemy raycastEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = target1;
        waiting = false;
        raycastEnemy = GetComponent<RaycastEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            towardsTarget = new Vector2(target.position.x, target.position.y) - rb.position;
            facingDirection = raycastEnemy.rayDirection;
            float angle = (Mathf.Atan2(towardsTarget.y, towardsTarget.x) - (Mathf.PI / 2)) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotationSpeed);
            if (transform.rotation.z < q.z + 0.01 && transform.rotation.z > q.z - 0.01)
            {
                StartCoroutine(TurnCycle());
            }
        }
    }

  public IEnumerator TurnCycle()
    {
        waiting = true;
        if(target == target1)
        {
            target = target2;
        }
        else
        {
            target = target1;
        }
        
        yield return new WaitForSecondsRealtime(1f);
        waiting = false;
    }
}
