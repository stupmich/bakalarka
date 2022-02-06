using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private LootTable lootTable;
    private GameObject player;
    private ExperienceManager expMan;

    public override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
        expMan = player.GetComponent<ExperienceManager>();
        lootTable = GetComponent<LootTable>();
    }

    public override void Die()
    {
        base.Die();

        if (lootTable != null)
        {
            lootTable.DropLoot(transform.position);
        }

        if (expMan != null)
        {
            expMan.AddXP(100);
        }

        StartCoroutine(waiter());
        //efekt, loot
        
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
}
