using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerSizeController : MonoBehaviour
{
    public Transform cat;
    public int playerSize=1;
    public CinemachineFreeLook cam;
    float initialTopRigHeight, initialTopRigRadius, initialMidRigHeight, initialMidRigRadius, initialBotRigHeight, initialBotRigRadius;

    public static PlayerSizeController Instance;
    public float zoomSensitivity,minZoom,maxZoom;
    float zoomAmt = 1;
    public TextMeshPro text;





    private int changeSpeed = 2;
    private float changeTime;

    private Vector3 sizeBefore;
    public void ChangeSize(int addValue) {
        playerSize += addValue;
        playerSize = Mathf.Clamp(playerSize, 1, 100000);

        sizeBefore = gameObject.transform.localScale;
        StartCoroutine(UpdatePlayerSize());
    }




    public IEnumerator UpdatePlayerSize() {
        changeTime = 0;
        float size= Mathf.Pow((float)playerSize, 1.0f/3.0f);
        text.text = "" + playerSize;
        //Debug.Log(size);
        while (Vector3.Distance(transform.localScale, new Vector3(size,size,size)) > 0.001){
            gameObject.transform.localScale = Vector3.Lerp(sizeBefore, new Vector3(size, size, size), changeTime);
            //Changes Camera Distances
            cam.m_Orbits[0].m_Height = initialTopRigHeight * zoomAmt * transform.localScale.x;
            cam.m_Orbits[0].m_Radius = initialTopRigRadius * zoomAmt * transform.localScale.x;
            cam.m_Orbits[1].m_Height = initialMidRigHeight * zoomAmt * transform.localScale.x;
            cam.m_Orbits[1].m_Radius = initialMidRigRadius * zoomAmt * transform.localScale.x;
            cam.m_Orbits[2].m_Height = initialBotRigHeight * zoomAmt * transform.localScale.x;
            cam.m_Orbits[2].m_Radius = initialBotRigRadius * zoomAmt * transform.localScale.x;
            //doesNextFrame
            yield return new WaitForFixedUpdate();
            changeTime += changeSpeed * Time.deltaTime;
        }
        gameObject.transform.localScale = new Vector3(size, size, size);
        cam.m_Orbits[0].m_Height = initialTopRigHeight * zoomAmt * transform.localScale.x;
        cam.m_Orbits[0].m_Radius = initialTopRigRadius * zoomAmt * transform.localScale.x;
        cam.m_Orbits[1].m_Height = initialMidRigHeight * zoomAmt * transform.localScale.x;
        cam.m_Orbits[1].m_Radius = initialMidRigRadius * zoomAmt * transform.localScale.x;
        cam.m_Orbits[2].m_Height = initialBotRigHeight * zoomAmt * transform.localScale.x;
        cam.m_Orbits[2].m_Radius = initialBotRigRadius * zoomAmt * transform.localScale.x;
    }

    private void Awake()
    {
        Instance = GetComponent<PlayerSizeController>();
    }



    // Start is called before the first frame update
    void Start()
    {
        changeTime = 0;
        text.text = "" + playerSize;
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
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //Debug.Log("SCROLLING");
            zoomAmt = Mathf.Clamp(zoomAmt - Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity, minZoom, maxZoom);
            StartCoroutine(UpdatePlayerSize());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Vector3.Distance(cat.position, transform.position) <= 2f)
            {
                Debug.Log("Petting Range");
                CatSizeController csc = cat.gameObject.GetComponent<CatSizeController>();
                int tmpsize = csc.storedSize;
                csc.storedSize = playerSize;
                playerSize = tmpsize;
                StartCoroutine(UpdatePlayerSize());
                csc.updateText();
            }
        }
    }
}
