using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimator : CharacterAnimator
{
    protected override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();

        combat.OnAttack += OnAttack;
        stats.OnDeath += OnDeath;
    }
}
