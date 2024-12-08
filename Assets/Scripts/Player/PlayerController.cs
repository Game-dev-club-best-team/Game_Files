using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    Vector2 moveInput;
    public float moveSpeed;
    float moveInputX;
    float velocityX;
    float velocityY;
    public Rigidbody2D rb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveSideways();
    }

    public void MoveSideways()
    {
        velocityX = moveInputX * Time.deltaTime * moveSpeed;
        velocityY = rb.velocity.y;
        rb.velocity = new Vector2(velocityX, velocityY);
        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveInputX = moveInput.x;
        Debug.Log(value);
    }


}
