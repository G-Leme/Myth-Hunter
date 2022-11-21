using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecond : MonoBehaviour
{
   public float health, maxHealth = 100f;
    [SerializeField] private CircleCollider2D circleCol;
  public bool howling;

    [SerializeField] private Renderer render;
    [SerializeField] private Color colorToTurnTo = Color.red;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackAudio;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip howlingAudio;
    [SerializeField] private TopDownMovement playerHealth;

    public float speed = 2.5f;
    public float attackRange = 6f;
    public Animator animator;
    Transform player;
    Rigidbody2D rb;
    Transform wereWolf;
    public bool canMove;
    public bool isDashing;
    Transform targetToJump;

    public bool chargingDash = true;
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLenght = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

   
    void Start()
    {
        // Referências

     

        audioSource = GetComponent<AudioSource>(); 

        Physics2D.IgnoreLayerCollision(3, 12);

        render = GetComponent<Renderer>();

        health = maxHealth;

        targetToJump = GameObject.FindGameObjectWithTag("TargetToJump").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        wereWolf = GameObject.FindGameObjectWithTag("WereWolf").transform;
        rb = animator.GetComponent<Rigidbody2D>();

        canMove = true;

        activeMoveSpeed = speed;
    }

    
    void Update()
    {
        if (playerHealth.currentHealth == 0)
        {
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(howlingAudio);
            playerHealth.currentHealth = -1;
            StartCoroutine(PlayerDeath());
        }


        animator.SetFloat("Horizontal", targetToJump.position.x - wereWolf.position.x);
        animator.SetFloat("Vertical", targetToJump.position.y - wereWolf.position.y);

        //Segue o Jogador
        Vector2 target = new Vector2(targetToJump.position.x, targetToJump.position.y);
        if (canMove == true)
        {

            Vector2 newPos = Vector2.MoveTowards(rb.position, target, activeMoveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

        }

        // Dash em direção do Jogador
         if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
          StartCoroutine(DashCoroutine());


            if (dashCoolCounter <= 0 && dashCounter <= 0 && chargingDash == false)
            {
                audioSource.PlayOneShot(attackAudio);
                animator.SetBool("Dashing", true);
                attackRange = 0;
                isDashing = true;
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLenght;
                Debug.Log("I am dashing");
            }
        }




        if (dashCoolCounter > 0)
        {
            animator.SetBool("Howling", true);
            howling = true;
            canMove = false;
        }
        else
        {

            animator.SetBool("Howling", false);
            canMove = true;
            howling = false;
            render.material.color = Color.white;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                
                isDashing = false;
                canMove = true;
                Debug.Log("i am walking");
                activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
                StartCoroutine(AttackRangeStopped());
            }
        }

        if (dashCoolCounter > 0)
        {

            dashCoolCounter -= Time.deltaTime;
        }


        if (isDashing == false)
        {
          
        }



    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<TopDownMovement>(out TopDownMovement playerComponent))
        {
            StartCoroutine(DamageCoroutine());
            playerComponent.TakeDamagePlayer(25);
        }

    }

        public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        audioSource.PlayOneShot(hurtAudio);
        render.material.color = colorToTurnTo;

        if (health == 50)
        {
            Debug.Log("Werewolf is Enraged");
        }
    }

    // Manipula Movimentação pós Dash
    IEnumerator AttackRangeStopped()
    {
        yield return new WaitForSeconds(3f);
        attackRange = 6f;
    }

    // Prepara o Dash
    IEnumerator DashCoroutine()
    {
        yield return null;
        canMove = false;

        yield return new WaitForSeconds(0.2f);
        chargingDash = false;
        canMove = true;
        yield return new WaitForSeconds(0.1f);
        chargingDash = true;
        animator.SetBool("Dashing", false);
        yield break;

    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(1f);
       
        animator.SetBool("Howling", true);
        this.enabled = false;
        yield break;
    }

    IEnumerator DamageCoroutine()
    {
        yield return null;
        Physics2D.IgnoreLayerCollision(3, 6);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(3, 6, false);
        yield break;
    }
}
