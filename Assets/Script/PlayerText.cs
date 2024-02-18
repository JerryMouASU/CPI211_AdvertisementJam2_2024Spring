using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour
{

    public Transform target; // The GameObject to tag
    public Vector3 offset = new Vector3(0, 1, 0); // Offset above the GameObject
    public Text tagText; // Reference to the UI Text element


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerSizeController.Instance.transform;
       // tagText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && tagText != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + Mathf.Pow(PlayerSizeController.Instance.playerSize,1.0f/3.0f )* offset);
            tagText.transform.position = screenPos;
            tagText.text = PlayerSizeController.Instance.playerSize.ToString() ;
        }
    }
}
