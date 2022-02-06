using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public int currentXP;
    public int toLevelXP;
    public int level;

    public int statPoints;
    public int abilityPoints;

    public static ExperienceManager instance;

    public event System.Action<int, int,int,int> OnXPChanged;
    public event System.Action<int> OnAbilityPointsChanged;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (OnXPChanged != null)
        {
            OnXPChanged(toLevelXP, currentXP, level, statPoints);
        }

        if (OnAbilityPointsChanged != null)
        {
            OnAbilityPointsChanged(abilityPoints);
        }
    }

    public void AddXP(int pXP)
    {
        currentXP += pXP;

        while (currentXP >= toLevelXP)
        {
            currentXP = currentXP - toLevelXP;
            level++;
            statPoints += 3;
            abilityPoints += 1;
            toLevelXP += toLevelXP / 20;
        }

        if (OnXPChanged != null)
        {
            OnXPChanged(toLevelXP, currentXP, level, statPoints);
        }

        if (OnAbilityPointsChanged != null)
        {
            OnAbilityPointsChanged(abilityPoints);
        }

    }

    public void UseStatPoint()
    {
        statPoints--;

        if (OnXPChanged != null)
        {
            OnXPChanged(toLevelXP, currentXP, level, statPoints);
        }
    }
}
