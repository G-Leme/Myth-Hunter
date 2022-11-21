using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecundarieAnimations : MonoBehaviour
{

    BossSecond bossCol;

    [SerializeField] Animator animator;
    [SerializeField] GameObject Boss;
    BossSecondPhase bossSpeed;



    private void Awake()
    {
        bossCol = Boss.GetComponent<BossSecond>();
        bossSpeed = Boss.GetComponent<BossSecondPhase>();
    }

    void Start()
    {
   

    }

    // Update is called once per frame
    void Update()
    {
        if (bossCol.isDashing == true)
        {
            animator.SetBool("IsDashing", true);
        }
        else
        {
            animator.SetBool("IsDashing", false);
        }

        if(bossSpeed.werewolfHurt == true)
        {
            animator.SetBool("IsHurt", true);
        }
        else if(bossSpeed.werewolfHurt == false)
        {
            animator.SetBool("IsHurt", false);
        }
    }
}
