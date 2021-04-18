using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    int time;
    int day;

    Camera camera;
    Animator animator;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    bool lastMoveLeft;
    bool lastMoveUp;
    bool lastMoveDown;
    bool lastMoveRight;
    public bool activeArea;

    public float runSpeed;
    bool isMoving;
    string prevAnimation;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        body = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    private void OnMouseOver()
    {
        activeArea = true;
    }
    private void OnMouseExit()
    {
        activeArea = false;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        if (horizontal !=0 || vertical != 0)
        {
            isMoving = true;
            animator.SetBool("IsMoving", isMoving);
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            lastMoveLeft = false;
            lastMoveUp = false;
            lastMoveDown = false;
            lastMoveRight = false;
            prevAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            //Debug.Log(prevAnimation);
        }
        else
        {
            switch (prevAnimation)
            {
                case "PlayerAnimated_walkLeft":
                    lastMoveLeft = true;
                    break;
                case "PlayerAnimated_walkRight":
                    lastMoveRight = true;
                    break;
                case "PlayerAnimated_walkUp":
                    lastMoveUp = true;
                    break;
                case "PlayerAnimated_walkDown":
                    lastMoveDown = true;
                    break;
            }
            isMoving = false;
        }
        animator.SetBool("lastMoveLeft", lastMoveLeft);
        animator.SetBool("lastMoveUp", lastMoveUp);
        animator.SetBool("lastMoveRight", lastMoveRight);
        animator.SetBool("lastMoveDown", lastMoveDown);
        animator.SetBool("IsMoving", isMoving);
        moveCamera();

    }

    void moveCamera()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            camera.orthographicSize  += 0.01f;
        }
        if (Input.GetKey(KeyCode.X))
        {
            camera.orthographicSize -= 0.01f;
        }
    }
    void FixedUpdate()
    {


        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
