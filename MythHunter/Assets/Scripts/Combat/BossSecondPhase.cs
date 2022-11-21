using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondPhase : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform secondPhase;
    [SerializeField] GameObject Boss;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform targetToJump;
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private Animator animator;
    [SerializeField] private Renderer render;
    [SerializeField] private Color colorToTurnTo = Color.red;
    [SerializeField] private AudioSource bushSound;
    [SerializeField] private AudioClip howlingAudio;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip attackAudio;

    public bool werewolfHurt;
    public bool werewolfDied;
    TopDownMovement playerHealth;
    BossSecond bossHealth;
    public float speed = 2.5f;
    public float jumpsAvailable = 1f;

    public GameObject[] spawnPoints;

    public bool canMove;


    private void Awake()
    {
        render = GetComponent<Renderer>();
        playerHealth = player.GetComponent<TopDownMovement>();
        bossHealth = Boss.GetComponent<BossSecond>();
    }

    void Start()
    {
       
        canMove = true;
        targetToJump = GameObject.FindGameObjectWithTag("TargetToJump").transform;
        secondPhase = GameObject.FindGameObjectWithTag("SecondPhaseTransform").transform;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
      
    }

  
    void FixedUpdate()
    {

        if(playerHealth.currentHealth == 0)
        {
            
                bushSound.volume = 0.1f;
                bushSound.PlayOneShot(howlingAudio);
                playerHealth.currentHealth = -1;
                StartCoroutine(PlayerDeath());
            
        }

     if(speed == 20)
        {
            werewolfHurt = true;
        }

        if (bossHealth.health == -10)
        {
            render.material.color = Color.white;
            bossHealth.isDashing = false;
            werewolfDied = true;
            Debug.Log("WereWolf Died");
            StopAllCoroutines();
            bushSound.PlayOneShot(howlingAudio);
            bushSound.volume = 0.1f;
            animator.SetTrigger("Death");
            bossHealth.health = 0;
        }

        if (bossHealth.health == 50)
        {

            
            bossHealth.enabled = false;

            StartCoroutine(SecondPhaseStartCoroutine());

        }

        if (transform.position == secondPhase.transform.position && canMove == true)
        {
            StopCoroutine(SecondPhaseStartCoroutine());

            StartCoroutine(ReturnPosition());

          
           
        }

        if (bossHealth.health == 40 && transform.position == secondPhase.transform.position)
        {
            StartCoroutine(MoveTowardsPlayer());
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
        bossHealth.health -= damageAmount;

          
      
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
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(3, 6, false);
        yield break;
    }

    IEnumerator SecondPhaseStartCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
      
        render.material.color = colorToTurnTo;
        speed = 20f;
        Physics2D.IgnoreLayerCollision(3, 6);
        Physics2D.IgnoreLayerCollision(3, 10);
        yield return new WaitForSeconds(2f);
        speed = 19f;
        werewolfHurt = false;
        animator.SetBool("Howling", false);
        animator.SetFloat("Vertical", secondPhase.transform.position.y);
        Vector2 SecondPhaseTransform = new Vector2(secondPhase.position.x, secondPhase.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, SecondPhaseTransform, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
      // Debug.Log("Second Phase Starting");     
        yield return new WaitForSeconds(0.4f);
        bossHealth.health = 40;
        Physics2D.IgnoreLayerCollision(3, 6, false);
        Physics2D.IgnoreLayerCollision(3, 10, false);
        yield break ;

    }

  IEnumerator ReturnPosition()
    {
        canMove = false;
        boxCol.enabled = false;
        yield return new WaitForSeconds (3f);
        render.material.color = Color.white;
        this.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].transform.position;
        canMove = true;
        bushSound.Play();
        yield return new WaitForSeconds(1.4f);
        bushSound.spatialBlend = 0f;
        bushSound.PlayOneShot(attackAudio);
        yield return new WaitForSeconds(1f);
        Debug.Log("I moved");
        yield return new WaitForSeconds(3f);
        animator.SetBool("Dashing", false);
        animator.SetBool("DoneDashing", true);
        animator.SetFloat("Horizontal", player.transform.position.x - transform.position.x);
        animator.SetFloat("Vertical", player.transform.position.y - transform.position.y);
        bossHealth.isDashing = false;
        transform.position = secondPhase.position;
        yield return new WaitForSeconds(0.2f);
        bushSound.spatialBlend = 1f;
        animator.SetBool("DoneDashing", false);
        yield break;
    }
    

    IEnumerator MoveTowardsPlayer()
    {
        yield return new WaitForSeconds(4.5f);
        bossHealth.isDashing = true;
        animator.SetBool("Dashing", true);
        animator.SetFloat("Horizontal", targetToJump.transform.position.x - transform.position.x);
        animator.SetFloat("Vertical", targetToJump.transform.position.y - transform.position.y);
        speed = 60f;
            Vector2 target = new Vector2(targetToJump.position.x, targetToJump.position.y);
            Vector2 pos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(pos);
        
    }

}
