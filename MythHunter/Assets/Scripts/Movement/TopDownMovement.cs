using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Shooting weapon;
    public Animator animator;
    public Camera cam;
    public int maxHealth = 100;
    public int currentHealth;
    public Texture2D cursorArrow;
     BossSecondPhase deathTrigger;
    public GameObject Boss;
    [SerializeField] private Renderer render;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public bool canMove;
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLenght = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        canMove = true;
        deathTrigger = Boss.GetComponent<BossSecondPhase>();

        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);

       

        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            render.material.color = Color.red;
            canMove = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.enabled = false;
            animator.SetFloat("Speed", 0);
        }

        if(deathTrigger.werewolfDied == true)
        {
            canMove = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetFloat("Speed", 0);
        }

        ProcessInputs();

        Dash();

        animator.SetFloat("Horizontal", mousePosition.x - transform.position.x);
        animator.SetFloat("Vertical",  mousePosition.y - transform.position.y );
        if (canMove == true)
        {
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);

            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }


    private void FixedUpdate()
    {
        
            rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);
        
    }

    public void TakeDamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    void ProcessInputs()
    {
       float moveX = Input.GetAxisRaw("Horizontal");
       float moveY = Input.GetAxisRaw("Vertical");

       
            moveDirection = new Vector2(moveX, moveY).normalized;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLenght;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
}
