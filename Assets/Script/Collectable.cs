using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectable : MonoBehaviour
{
    [SerializeField]private int sizeChangeAmount=2;
    //public Transform target; // The GameObject to tag
    public Vector3 offset = new Vector3(0, 1, 0); // Offset above the GameObject
    //public Text tagText; // Reference to the UI Text element
    public TextMeshPro text;

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(" HEY");
        if (collision.gameObject.CompareTag("Player")) {
            PlayerSizeController.Instance.ChangeSize(sizeChangeAmount);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //target = this.transform;
        //tagText.text = ""+sizeChangeAmount;
        text.text = "" + sizeChangeAmount;
        switch (Mathf.Sign(sizeChangeAmount))
        {
            case 1:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case -1:
                GetComponent<Renderer>().material.color = Color.red;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (target != null && tagText != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + Mathf.Pow(PlayerSizeController.Instance.playerSize, 1.0f / 3.0f) * offset);
            tagText.transform.position = screenPos;
            if (Vector3.Distance(target.position, PlayerSizeController.Instance.transform.position) > 13) { tagText.enabled=false; }
            else tagText.enabled = true;

        }*/
    }
}
