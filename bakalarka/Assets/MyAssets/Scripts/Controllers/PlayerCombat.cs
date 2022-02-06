using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    public PlayerStats playerStats;

    void Start ()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        base.myStats = playerStats;
    }


    public override void Attack(CharacterStats targetStats)
    {
        if (base.attackCoolDown <= 0f && playerStats.rage < playerStats.maxRage)
        {
            playerStats.SetRage(20);
        }
        base.Attack(targetStats);     
    }

}
