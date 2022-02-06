using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat enemyCombat;
    protected CharacterStats stats;

    void Start()
    {

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.died == false)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();

                    if (targetStats != null)
                    {

                        enemyCombat.Attack(targetStats);
                    }
                    FaceTarget();
                }
            }
        }

    }

    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
