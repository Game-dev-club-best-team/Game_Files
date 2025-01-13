using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target;
    public float rotationSpeed;
    public Rigidbody2D rb;
    public bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = target1;
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = target.localPosition.normalized;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        if(rb.angularVelocity <= 0.1 && waiting == false)
        {
            StartCoroutine(TurnCycle());
        }
        Debug.Log(targetDirection);
        Debug.Log(toRotation);
    }

  public IEnumerator TurnCycle()
    {
        if(target == target1)
        {
            target = target2;
        }
        else
        {
            target = target1;
        }
        waiting = true;
        yield return new WaitForSecondsRealtime(1.5f);
        waiting = false;
    }
}
