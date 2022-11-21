using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    Transform player;
    Rigidbody2D rb;
    public bool canMove = true;
    [SerializeField] private CircleCollider2D circleCol;

    BossSecond bossDash;

   [SerializeField] GameObject Boss;


    private void Awake()
    {
      bossDash = Boss.GetComponent<BossSecond>();
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 6);
        Physics2D.IgnoreLayerCollision(7, 9);
        Physics2D.IgnoreLayerCollision(7, 12);

        circleCol = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {


        if (bossDash.isDashing == true)
        {
           StartCoroutine(StopFolloPlayer());
        }
        else if (bossDash.isDashing == false)
        {
            StopCoroutine(StopFolloPlayer());
            canMove = true;
            circleCol.enabled = false;
        }
      //  Vector2 target = new Vector2(player.position.x, player.position.y);

        if (canMove == true)
        {
            gameObject.transform.position = player.position;

            rb.velocity = Vector3.zero;
          //  Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            //rb.MovePosition(newPos);
        }
    }

     IEnumerator  StopFolloPlayer()
    {
  
        yield return null;
        canMove = false;
        circleCol.enabled = true;
        yield return new WaitForSeconds(1f);

    }
}
