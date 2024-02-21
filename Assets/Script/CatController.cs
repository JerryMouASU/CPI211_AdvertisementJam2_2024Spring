using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CatController : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject particleEffectPrefab;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                if (agent.SetDestination(hit.point)) {

                    Instantiate(particleEffectPrefab, hit.point, Quaternion.identity);
                    //get 
                }
            }
        }
        if (Vector3.Distance(transform.position,agent.destination) > 0.1)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }
}
