using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSprite : MonoBehaviour
{

    public Animator animator;
    public GameObject parent;
    public PatrolNavigation navigationLink;
    public SpriteRenderer spriteRenderer;
    public float velocityX;
    public float velocityY;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position;
        velocityX = navigationLink.facingDirection.x;
        velocityY = navigationLink.facingDirection.y;
        if(velocityX > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat("velocityY", velocityY);
        animator.SetFloat("velocityX", Mathf.Abs(velocityX));
        
    }
}
