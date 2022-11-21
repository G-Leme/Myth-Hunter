using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private Transform firePoint = null;
    [SerializeField] private GameObject bulletPrefab = null;

    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private bool canShot;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float nextFire = 0f;
    [SerializeField] private AudioSource crossbowSound;
    TopDownMovement playerHealth;
    [SerializeField] private GameObject player;
    BossSecond bossHealth;
    [SerializeField] private GameObject boss;
    private void Start()
    {
        bossHealth = boss.GetComponent<BossSecond>();
        playerHealth = player.GetComponent<TopDownMovement>();
        canShot = true;
        crossbowSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.currentHealth <= 0 || bossHealth.health <= 0)
        {
            canShot = false;
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && canShot == true)
        {
            crossbowSound.Play();
            Shoot();
        }
    }

   private void Shoot()
    {
        
        nextFire = Time.time + fireRate;
      GameObject bullet =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
      Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
      rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);   
    }
}
