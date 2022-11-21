using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private bool interactionRange = false;
    private InteractiveCanva canva;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canva != null && Input.GetKeyDown("e"))
        {
            canva.EnterLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractiveCanva>() != null && !interactionRange)
        {
            canva = collision.GetComponent<InteractiveCanva>();
            interactionRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InteractiveCanva>() != null)
        {
            canva = null;
            interactionRange = false;
        }
        
    }

}
