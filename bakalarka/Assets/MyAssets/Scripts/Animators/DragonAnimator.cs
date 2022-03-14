using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimator : CharacterAnimator
{
    private int attackCounter = 0;
    public GameObject flame;
    public Transform head;

    protected override void Update()
    {
        if (stats.died == false)
        {
            float speedPercent = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("SpeedPercent", speedPercent, .1f, Time.deltaTime);

            if (!animator.GetBool("InCombat") && combat.InCombat)
            {
                animator.SetTrigger("Scream");
            }
            animator.SetBool("InCombat", combat.InCombat);
        }
    }

    protected override void OnAttack()
    {
        if (stats.died == false)
        {
            animator.SetTrigger("Attack");
            if (attackCounter % 5  == 4)
            {
                Vector3 rot = head.rotation.eulerAngles;
                rot = new Vector3(rot.x, rot.y, rot.z + 10);
                GameObject childObject = Instantiate(flame, head.position, Quaternion.Euler(rot)) as GameObject;
                childObject.transform.parent = head.transform;

                overrideController[replaceableAttackAnimation.name] = currentAttackAnimationSet[2];
            } else if (attackCounter % 5 == 3)
            {
                overrideController[replaceableAttackAnimation.name] = currentAttackAnimationSet[1];
                combat.attackSpeed = 0.3f;
            } else
            {
                overrideController[replaceableAttackAnimation.name] = currentAttackAnimationSet[0];
                combat.attackSpeed = 0.6f;
            }
            attackCounter++;
        }
    }

}
