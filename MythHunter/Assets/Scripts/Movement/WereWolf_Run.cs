using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolf_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 5f;
    public Animator animator;
    Transform player;
    Rigidbody2D rb;
    Transform wereWolf;
    public bool canMove;
    public bool isDashing;
    Transform targetToJump;
    BossSecond bossHealth;
    [SerializeField] GameObject Boss;


    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLenght = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

  

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      bossHealth = Boss.GetComponent<BossSecond>();

      targetToJump = GameObject.FindGameObjectWithTag("TargetToJump").transform;

      player = GameObject.FindGameObjectWithTag("Player").transform;

      wereWolf = GameObject.FindGameObjectWithTag("WereWolf").transform;
      rb = animator.GetComponent<Rigidbody2D>();

        canMove = true;

        activeMoveSpeed = speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Vector2 target = new Vector2(targetToJump.position.x, targetToJump.position.y);
        //if (canMove == true)
        
            
           // Vector2 newPos = Vector2.MoveTowards(rb.position, target, activeMoveSpeed * Time.fixedDeltaTime);
            //rb.MovePosition(newPos);
               
        
        
      
        //animator.SetFloat("Speed", movement.sqrMagnitude);

       if(bossHealth.health == 50)
        {
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", 0);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

  
}
