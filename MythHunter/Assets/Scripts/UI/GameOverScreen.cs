using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Player;
    BossSecond bossHealth;
    TopDownMovement playerHealth;


    // Start is called before the first frame update
    void Start()
    {
        bossHealth = Boss.GetComponent<BossSecond>();
        playerHealth = Player.GetComponent<TopDownMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.health <= 0)
        {
            StartCoroutine(VictoryScreen());
        }
        else if(playerHealth.currentHealth <= 0)
        {
            animator.SetTrigger("IsDead");
        }

    }

    IEnumerator VictoryScreen()
    {

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("EndGame");
        yield break;
    }


}
