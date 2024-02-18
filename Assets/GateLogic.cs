using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour
{
    [SerializeField] private int sizeTarget;
    [SerializeField] private bool biggerOrEqual = true;
    private int playerSize;


    [SerializeField] private Material material;
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
    }



    // Start is called before the first frame update
    void Start()
    {
        //   material = GetComponent<Renderer>().material;
        color = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //playerSize = Player.Instance.;
        if (CheckIfPass(playerSize))
        {
            MatTrans();
            Collider s = GetComponent<Collider>();
            s.enabled = false;
        }
    }
}
