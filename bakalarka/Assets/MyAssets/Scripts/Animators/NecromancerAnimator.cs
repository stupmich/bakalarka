using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NecromancerAnimator : CharacterAnimator
{
    [SerializeField] ParticleSystem attackParticle = null;

    protected override void OnAttack()
    {
        animator.SetTrigger("Attack");
        attackParticle.Play();
    }
}
