using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void moveToPoint(Vector3 point, Interactable focus)
    {
        if (focus != null)
        {
            agent.stoppingDistance = focus.radius * .6f;
        } else
        {
            agent.stoppingDistance = 0f;
        }

        agent.SetDestination(point);
    }
}
