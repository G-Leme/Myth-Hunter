using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWerewolf : MonoBehaviour
{
    private Transform werewolf;

    BossSecond bossCol;

    [SerializeField] GameObject Boss;

  [SerializeField]  private BoxCollider2D boxCol;

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
    

        transform.position = werewolf.position; 

        if(bossCol.howling == true)
        {
            boxCol.enabled = false;
        }
        else
        {
            boxCol.enabled = true;
        }
    }
}
