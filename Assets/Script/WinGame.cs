using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject winningMenu;
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(" HEY");
        if (collision.gameObject.CompareTag("Player"))
        {
            winningMenu.SetActive(true);
           // PlayerSizeController.Instance.ChangeSize(sizeChangeAmount);

            // Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        winningMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
