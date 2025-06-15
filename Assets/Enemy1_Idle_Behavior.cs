using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Idle_Behavior : StateMachineBehaviour
{
    Rigidbody2D rigidbody2;
    Transform player;
    float speed = 3f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody2 = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().LookAtPlayer();
        if (Vector2.Distance(player.position, rigidbody2.position) <= 15)
        {
            animator.SetBool("Chase", true);
        }
        else if(Vector2.Distance(player.position, rigidbody2.position) <= 5)
        {
            animator.SetTrigger("Attack");
        }
        else if(Vector2.Distance(player.position, rigidbody2.position) >= 15)
        {
            animator.SetBool("Chase", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
