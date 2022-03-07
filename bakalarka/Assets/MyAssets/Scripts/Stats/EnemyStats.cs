using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private LootTable lootTable;
    private GameObject player;
    private ExperienceManager expMan;
    public int ID;

    public static event System.Action<int> OnEnemyDeath;
    public static event System.Action<GameObject> ChangedState;

    public override void Start()
    {
        if (this.died)
        {
            Object.Destroy(this.gameObject);
        } else
        {
            base.Start();
            player = GameObject.FindWithTag("Player");
            expMan = player.GetComponent<ExperienceManager>();
            lootTable = GetComponent<LootTable>();
        }
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

        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(ID);
        }

        if (ChangedState != null)
        {
            ChangedState(this.gameObject);
        }

        StartCoroutine(waiter());
        
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(4);
        this.gameObject.SetActive(false);
    }
}
