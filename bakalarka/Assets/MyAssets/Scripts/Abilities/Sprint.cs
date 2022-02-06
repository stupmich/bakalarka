using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[CreateAssetMenu]
public class Sprint : Ability
{
    public float sprintVelocity;

    public override void Activate(GameObject parent, int level)
    {
        NavMeshAgent navMeshAgent = parent.GetComponent<NavMeshAgent>();

        navMeshAgent.speed += (sprintVelocity + level);
    }

    public override void Deactivate(GameObject parent, int level)
    {
        NavMeshAgent navMeshAgent = parent.GetComponent<NavMeshAgent>();

        navMeshAgent.speed -= (sprintVelocity + level);
    }

    public override void LevelUp()
    {
        sprintVelocity++;
    }
}
