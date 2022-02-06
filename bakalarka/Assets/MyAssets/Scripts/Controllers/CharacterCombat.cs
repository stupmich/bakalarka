using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    protected CharacterStats myStats;
    public float attackSpeed = 1f;
    protected float attackCoolDown = 0f;
    public float attackDelay = .6f;
    protected const float combatCooldown = 5f;
    protected float lastAttackTime;

    public event System.Action OnAttack;

    //public bool InCombat { get; private set; }
    public bool InCombat;

    void Start ()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update ()
    {
        attackCoolDown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown )
        {
            InCombat = false;
        }
    }

    public virtual void Attack (CharacterStats targetStats)
    {

        if (attackCoolDown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();

            attackCoolDown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
        if (stats.currentHealth <= 0 )
        {

            InCombat = false;
        }
    }
}
