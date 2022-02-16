using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnimation;
    public AnimationClip[] defaultAttackAnimationSet;
    protected AnimationClip[] currentAttackAnimationSet;

    protected Animator animator;
    protected NavMeshAgent agent;
    protected CharacterCombat combat;
    protected CharacterStats stats;
    public AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }

        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimationSet = defaultAttackAnimationSet;
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
            int attackIndex = Random.Range(0, currentAttackAnimationSet.Length);
            overrideController[replaceableAttackAnimation.name] = currentAttackAnimationSet[attackIndex];
        }
    }

    protected virtual void OnDeath()
    {
        animator.SetTrigger("Death");
    }
}
