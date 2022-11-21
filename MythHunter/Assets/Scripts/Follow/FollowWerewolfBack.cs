using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWerewolfBack : MonoBehaviour
{
    private Transform werewolf;

    BossSecond bossCol;

    [SerializeField] GameObject Boss;
    [SerializeField] BoxCollider2D boxCol;



    private void Awake()
    {
        bossCol = Boss.GetComponent<BossSecond>();
    }

    void Start()
    {
        werewolf = GameObject.FindGameObjectWithTag("WereWolf").transform;

    }

    // Update is called once per frame
    void Update()
    {
  

        if(bossCol.health == 50)
        {
            boxCol.enabled = false;
        }

        transform.position = werewolf.position;


    }
}
