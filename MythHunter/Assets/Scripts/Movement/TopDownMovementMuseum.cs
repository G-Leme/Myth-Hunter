using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementMuseum : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public Camera cam;
    public Texture2D cursorArrow;
    [SerializeField] private AudioSource walkSound;

    private Vector2 moveDirection;
    private Vector2 mousePosition;


    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLenght = .5f, dashCooldown = 1f;


    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);

        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();


        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical",moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);

        if(rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }
        else
        {
            walkSound.Stop();
        }

    }


    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

       
        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
