using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform tdestination1;
    [SerializeField] private Transform tdestination2;
    private Transform currentDest;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(tdestination1.position);
        currentDest = tdestination1;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(agent.transform.position, currentDest.position);
        if (distance <= 1.2f)
        {
            Debug.Log("Reached Destination 1");
            if (currentDest == tdestination1)
            {
                currentDest = tdestination2;
            }
            else
            {
                currentDest = tdestination1;
            }
            agent.SetDestination(currentDest.position);
        }
        else
        {
            Debug.Log("Distance is : " + distance);
        }
    }
}
