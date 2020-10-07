using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.Play("walkLeft");
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.Play("walkRight");
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.Play("walkUp");
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            animator.Play("walkDown");
        }
    }
}
