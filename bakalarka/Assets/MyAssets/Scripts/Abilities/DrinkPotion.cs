using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DrinkPotion : Ability
{
    public int healthRegen;
    PlayerStats stats;

    public override void Activate(GameObject parent, int level)
    {
        stats = parent.GetComponent<PlayerStats>();

        if (stats != null)
        {
            stats.Heal(healthRegen + (level * 10));
        }
    }

}
