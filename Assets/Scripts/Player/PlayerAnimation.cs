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
        animator.GetFloat("Horizontal");
        animator.GetFloat("Vertical");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
