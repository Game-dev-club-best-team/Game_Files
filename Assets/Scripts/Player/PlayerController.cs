using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
 
    Vector2 moveInput;
   
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public float sprintMod;
    public float moveSpeed;  
    
    float moveInputX;
    float moveInputY;
    float velocityX;
    float velocityY;

    bool touchScrap;

    Invintory invintory;

    InputAction sprint;

    void Start()
    {
        sprint = playerInput.actions.FindAction("Sprint");
        invintory = gameObject.GetComponent<Invintory>();
    }

    // Update is called once per frame
    void Update()
    {
           if (sprint.WasReleasedThisFrame())
        {
            moveSpeed = moveSpeed / (sprintMod);
        }
    }

    private void FixedUpdate()
    {
        MoveSideways();
   
    }

    public void MoveSideways()
    {
        velocityX = moveInputX * Time.deltaTime * moveSpeed;
        velocityY = moveInputY * Time.deltaTime * moveSpeed;
        rb.velocity = new Vector2(velocityX, velocityY);
        

    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveInputX = moveInput.x;
        moveInputY = moveInput.y;
    }

    public void OnSprint()
    {
        moveSpeed *= sprintMod;
        
    }

    public void OnInteract()
    {
        Debug.Log("Epic");
        if(touchScrap)
        {

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.tag;
        if (tag.Equals("Scrap")){
        touchScrap = true;
        }         
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        var tag = collision.tag;
        if (tag.Equals("Scrap"))
            touchScrap = true;
    }
}
