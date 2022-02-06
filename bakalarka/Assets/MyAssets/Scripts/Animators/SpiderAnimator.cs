using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimator : MonoBehaviour
{
    protected Animator animator;
    UnityEngine.AI.NavMeshAgent agent;
    protected CharacterCombat combat;
    protected CharacterStats stats;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();

        combat.OnAttack += OnAttack;
        stats.OnDeath += OnDeath;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stats.died == false)
        {
            float speedPercent = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("SpeedPercent", speedPercent, .1f, Time.deltaTime);
            animator.SetBool("InCombat", combat.InCombat);
        }
    }

    protected virtual void OnAttack()
    {
        if (stats.died == false)
        {
            animator.SetTrigger("Attack");
        }

    }

    protected virtual void OnDeath()
    {
        animator.SetTrigger("Death");
    }
}
