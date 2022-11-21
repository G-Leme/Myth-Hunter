using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    BossSecond bossHealth;
    [SerializeField] GameObject Boss;

    private void Awake()
    {
        bossHealth = Boss.GetComponent<BossSecond>();
    }

    private void Start()
    {
         Physics2D.IgnoreLayerCollision(10, 6);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if(collision.gameObject.TryGetComponent<BossSecond>(out BossSecond bossComponent))
        {
            bossComponent.TakeDamage(50);
        }

        Destroy(gameObject);

         
    }

    public void Update()
    {
        Destroy(gameObject, 5f);
    }

}
