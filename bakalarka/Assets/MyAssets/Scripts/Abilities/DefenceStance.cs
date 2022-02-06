using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DefenceStance : Ability
{
    public int armorModifier;
    public int vitalityModifier;
    PlayerStats stats;

    public GameObject effect;
    GameObject effectObj;

    public override void Activate(GameObject parent, int level)
    {
        stats = parent.GetComponent<PlayerStats>();

        if (stats != null)
        {
            stats.AddModifierArmor(armorModifier + level);
            stats.AddModifierVitality(vitalityModifier + level);
        }

        effectObj = Instantiate(effect, parent.transform.position, parent.transform.rotation) as GameObject;
        effectObj.transform.parent = parent.transform;
    }

    public override void Deactivate(GameObject parent, int level)
    {
        if (stats != null)
        {
            stats.RemoveModifierArmor(armorModifier + level);
            stats.RemoveModifierVitality(vitalityModifier + level);
        }

        Destroy(effectObj);
    }

    public override void LevelUp()
    {
        armorModifier++;
        vitalityModifier++;
    }
}
