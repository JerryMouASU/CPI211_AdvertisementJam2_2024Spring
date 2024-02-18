using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSizeController : MonoBehaviour
{

    public int playerSize=1;
    public CinemachineFreeLook cam;
    public float initialTopRigHeight, initialTopRigRadius, initialMidRigHeight, initialMidRigRadius, initialBotRigHeight, initialBotRigRadius;






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
        StartCoroutine(UpdatePlayerSize());
    }




    public IEnumerator UpdatePlayerSize() {
        float size= Mathf.Pow((float)playerSize, 1.0f/3.0f);
        Debug.Log(size);
        while (Vector3.Distance(transform.localScale, new Vector3(size,size,size)) > 0.001){
            gameObject.transform.localScale = Vector3.Lerp(sizeBefore, new Vector3(size, size, size), changeTime);
            //Changes Camera Distances
            cam.m_Orbits[0].m_Height = initialTopRigHeight * transform.localScale.x;
            cam.m_Orbits[0].m_Radius = initialTopRigRadius * transform.localScale.x;
            cam.m_Orbits[1].m_Height = initialMidRigHeight * transform.localScale.x;
            cam.m_Orbits[1].m_Radius = initialMidRigRadius * transform.localScale.x;
            cam.m_Orbits[2].m_Height = initialBotRigHeight * transform.localScale.x;
            cam.m_Orbits[2].m_Radius = initialBotRigRadius * transform.localScale.x;
            //doesNextFrame
            yield return new WaitForEndOfFrame();
            changeTime += changeSpeed * Time.deltaTime;
        }
        gameObject.transform.localScale = new Vector3(size, size, size);
    }
    // Start is called before the first frame update
    void Start()
    {
         changeTime=0;
    sizeBefore = gameObject.transform.localScale;
        initialTopRigHeight = cam.m_Orbits[0].m_Height;
        initialTopRigRadius = cam.m_Orbits[0].m_Radius;
        initialMidRigHeight = cam.m_Orbits[1].m_Height;
        initialMidRigRadius = cam.m_Orbits[1].m_Radius;
        initialBotRigHeight = cam.m_Orbits[2].m_Height;
        initialBotRigRadius = cam.m_Orbits[2].m_Radius;
    }

    // Update is called once per frame
    void Update()
    {
       // UpdatePlayerSize();
    }
}
