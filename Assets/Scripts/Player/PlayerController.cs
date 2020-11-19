using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    int time;
    int day;


    Animator animator;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed;
    bool isMoving;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        if(horizontal !=0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;    
        }
        animator.SetBool("IsMoving", isMoving);
        
        //implement use button based on current item
        //need to import player inventory
    }

    void FixedUpdate()
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
