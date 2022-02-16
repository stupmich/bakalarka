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

    public Animation fightStartAnim;
    public AnimationClip clip;
    private int frames;
    private bool cantMove = false;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();

        if (clip != null)
        {
            frames = Mathf.CeilToInt(clip.frameRate * clip.length);
            cantMove = true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.died == false)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                enemyCombat.InCombat = true;

                if (cantMove)
                {
                    if (frames > 0)
                    {
                        frames--;
                    } else if (frames == 0)
                    {
                        frames = -1;
                        this.cantMove = false;
                    } 
                }
                else
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

    IEnumerator Waiter(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }

    public void SetCantMove(bool pCantMove)
    {
        this.cantMove = pCantMove;
    }
}
