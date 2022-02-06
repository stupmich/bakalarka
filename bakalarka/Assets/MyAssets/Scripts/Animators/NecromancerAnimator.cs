using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NecromancerAnimator : MonoBehaviour
{
    protected Animator animator;
    NavMeshAgent agent;
    protected CharacterCombat combat;

    [SerializeField] ParticleSystem attackParticle = null;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
            
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", speedPercent, .1f, Time.deltaTime);
        animator.SetBool("InCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("Attack");
        attackParticle.Play();
    }


}
