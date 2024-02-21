using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateLogic : MonoBehaviour
{
    [SerializeField] private int sizeTarget;
    [SerializeField] private bool biggerOrEqual = true;
    private int playerSize;
    public TextMeshPro text;

    private Material material;
    private Color color;


    /// <summary>
    /// Check if the player can pass the gate.
    /// </summary>
    /// <param name="playerS">player's Size value</param>
    /// <returns></returns>
    private bool CheckIfPass(int playerS)
    {
        if (biggerOrEqual)
        {
            if (playerS >= sizeTarget)
            {
                return true;

            }
            else return false;
        }
        else
        {
            if (playerS <= sizeTarget)
            {
                return true;
            }
            else return false;
        }
    }

    /// <summary>
    /// Make the material transparent;
    /// </summary>
    void MatTrans()
    {
        color.a = 0.2f;
        material.color = color;
    }
    /// <summary>
    /// Make the material Opaque
    /// </summary>
    void MatOpaque()
    {
        color.a = 1f;
        material.color = color;
    }


    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().materials[1];
        switch (biggerOrEqual)
        {
            case true:
                text.text = ">=" + sizeTarget;
                color = Color.green;
                break;
            case false:
                text.text = "<=" + sizeTarget;
                color = Color.red;
                break;
        }
        color.a = 1f ;
        material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerSizeController.Instance!=null) playerSize = PlayerSizeController.Instance.playerSize;

        if (CheckIfPass(playerSize))
        {
            MatTrans();
            Collider s = GetComponent<Collider>();
            s.enabled = false;
        }
        else
        {
            MatOpaque();
            Collider s = GetComponent<Collider>();
            s.enabled = true;
        }
    }
}
