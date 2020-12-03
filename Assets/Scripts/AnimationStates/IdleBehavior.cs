using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    int randomCounter;
    private Vector2 forceToMove;
    private Vector2 forceToMovePixel;
    private Rigidbody2D rb;
    public float speed;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        Debug.Log(rb);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(randomCounter >= 100)
        {
            forceToMove = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            forceToMovePixel = new Vector2(Mathf.RoundToInt(forceToMove.x * 16), Mathf.RoundToInt(forceToMove.y * 16))/16;

            randomCounter = 0;
        }
        else
        {
            randomCounter += Random.Range(1, 5);
        }
        rb.AddForce(forceToMovePixel * speed * Time.deltaTime);
        animator.SetBool("isChasing", animator.gameObject.GetComponent<EnemyAICollision>().isNearPlayer);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
