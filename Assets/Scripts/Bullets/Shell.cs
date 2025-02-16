using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    float xVelocity;
    float yVelocity;
    float angularMomentum;
    void Start()
    {
        xVelocity = Random.Range(-100f, 100f);
        yVelocity = Random.Range(-10f, 50f);
        angularMomentum = Random.Range(-2000f, 2000f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2 (xVelocity, yVelocity));
        rb.AddTorque(angularMomentum);
        StartCoroutine(destroySelf());
    }

    // Update is called once per frame
    public IEnumerator destroySelf()
    {
        yield return new WaitForSecondsRealtime(15);
        Destroy(gameObject);
    }
}
