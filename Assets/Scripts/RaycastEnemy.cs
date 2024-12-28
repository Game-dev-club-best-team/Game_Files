using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEnemy : MonoBehaviour
{
    RaycastHit2D hit;
    public bool TargetHit;
    LayerMask mask;
    float rotationRad;
    public Vector2 rayDirection;
    void Start()
    {
        mask = LayerMask.GetMask("Player", "Walls");
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationRad = transform.eulerAngles.z;
        rotationRad = (Mathf.Deg2Rad * rotationRad) + (Mathf.PI / 2);
        rayDirection = new Vector2(Mathf.Cos(rotationRad), Mathf.Sin(rotationRad));
        hit = Physics2D.Raycast(transform.position,rayDirection, 100, mask);
        if (hit && hit)
        {
            Debug.Log(hit.collider.name);
        }
        TargetHit = hit;
       // Debug.Log(rayDirection);
    }
}
