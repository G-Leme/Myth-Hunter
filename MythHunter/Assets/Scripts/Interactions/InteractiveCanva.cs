using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCanva : MonoBehaviour
{
    private LevelChanger levelChanger;

    [SerializeField]
    private int levelId;

    // Start is called before the first frame update
    void Start()
    {
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterLevel()
    {
        levelChanger.FadeToLevel(levelId);
    }

}
