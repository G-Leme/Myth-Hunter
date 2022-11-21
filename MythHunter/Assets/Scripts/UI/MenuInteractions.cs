using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuInteractions : MonoBehaviour
{
    public Texture2D cursorArrow;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameAfterAnimation(float animationTime)
    {
        StartCoroutine(StartGameAfertSeconds(animationTime));
    }

    IEnumerator StartGameAfertSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

}
