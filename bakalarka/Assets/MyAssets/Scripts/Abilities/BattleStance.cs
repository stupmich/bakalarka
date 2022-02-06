using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleStance : Ability
{
    public int dexterityModifier;
    public int strengthModifier;
    PlayerStats stats;

    public GameObject effect;
    GameObject effectObj;
    public override void Activate(GameObject parent, int level)
    {
        stats = parent.GetComponent<PlayerStats>();

        if (stats != null)
        {
            stats.AddModifierStrength(strengthModifier + level);
            stats.AddModifierDexterity(dexterityModifier + level);
        }

        effectObj = Instantiate(effect, parent.transform.position, parent.transform.rotation) as GameObject;
        effectObj.transform.parent = parent.transform;
    }

    public override void Deactivate(GameObject parent, int level)
    {
        if (stats != null)
        {
            stats.RemoveModifierStrength(strengthModifier + level);
            stats.RemoveModifierDexterity(dexterityModifier + level);
        }
        Destroy(effectObj);
    }

    public override void LevelUp()
    {
        dexterityModifier++;
        strengthModifier++;
    }
}
