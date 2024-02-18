using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeController : MonoBehaviour
{

    public int playerSize=1;








    private int changeSpeed = 2;
    private float changeTime;

    private Vector3 sizeBefore;
    public void ChangeSize(int addValue) {
        if (playerSize + addValue >= 1)
        {
            playerSize += addValue;
        }
        else { playerSize = 1; }

        changeTime = 0;
        sizeBefore = gameObject.transform.localScale;
    }




    public void UpdatePlayerSize() {
        if (changeTime < 1) {
            changeTime += changeSpeed * Time.deltaTime;
        }
        float size= Mathf.Pow((float)playerSize, 1.0f/3.0f);
        Debug.Log(size);
        gameObject.transform.localScale = Vector3.Lerp(sizeBefore,new Vector3(size, size, size), changeTime);


    }
    // Start is called before the first frame update
    void Start()
    {
         changeTime=0;
    sizeBefore = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerSize();
    }
}
