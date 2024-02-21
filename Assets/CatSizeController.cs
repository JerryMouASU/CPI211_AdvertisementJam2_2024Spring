using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatSizeController : MonoBehaviour
{
    public int storedSize = 1;
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "" + storedSize;
    }
    public void updateText()
    {
        text.text = "" + storedSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
