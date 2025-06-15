using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Chase_Behavior : StateMachineBehaviour
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
        Vector2 target = new Vector2(player.position.x, rigidbody2.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody2.position, target, speed * Time.fixedDeltaTime);
        rigidbody2.MovePosition(newPos);
        animator.SetBool("Chase", true); 
        if (Vector2.Distance(player.position, rigidbody2.position) <= 5)
        {
            rigidbody2.MovePosition(this.rigidbody2.position);
            animator.SetTrigger("Attack");
            animator.SetBool("Chase", false);
        }
        else if (Vector2.Distance(player.position, rigidbody2.position) >= 15)
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
