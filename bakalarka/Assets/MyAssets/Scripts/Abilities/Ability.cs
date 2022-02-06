using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public Sprite abilityImage;
    public int rageCost;

    public virtual void Activate(GameObject parent, int level)
    {

    }

    public virtual void Deactivate(GameObject parent, int level)
    {
    }

    public virtual void LevelUp()
    {
    }

    public int GetRageCost()
    {
        return rageCost;
    }
}
