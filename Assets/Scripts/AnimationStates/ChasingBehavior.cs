using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBehavior : StateMachineBehaviour
{
    private Transform playerPosition;
    public float speed;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        animator.SetBool("isIdle", false);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", animator.gameObject.GetComponent<EnemyAICollision>().isNearPlayer);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(Mathf.RoundToInt(playerPosition.position.x * 16), Mathf.RoundToInt(playerPosition.position.y * 16)) / 16 , speed * Time.deltaTime);
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
