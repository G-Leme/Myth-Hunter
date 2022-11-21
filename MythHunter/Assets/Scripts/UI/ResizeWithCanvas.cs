using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeWithCanvas : MonoBehaviour
{
    private RectTransform canvasTransform;
    private RectTransform objTransform;

    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        objTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Resizer();
    }

    private void Resizer()
    {
        objTransform.sizeDelta = canvasTransform.sizeDelta;
    }

}
