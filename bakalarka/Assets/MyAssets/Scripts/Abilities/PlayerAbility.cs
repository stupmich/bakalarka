using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerAbility : ScriptableObject
{
    public Ability ability;
    float cooldownTime;
    float activeTime;
    public int level;
    private int maxLevel;
    private PlayerStats stats;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;

    public KeyCode key;

    private void Awake()
    {
        stats = PlayerStats.instance;
        level = 0;
        maxLevel = 5;
    }

    public void Activate(GameObject player)
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key) && stats.GetRage() >= ability.GetRageCost())
                {
                    stats.SetRage(-1 * ability.GetRageCost());
                    ability.Activate(player, level);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                    cooldownTime = ability.cooldownTime;
                } else if (Input.GetKeyDown(key) && stats.GetRage() < ability.GetRageCost())
                {
                    Debug.Log("Not enough rage!");
                }
                break;
            case AbilityState.active:
                cooldownTime -= Time.deltaTime;
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    ability.Deactivate(player, level);
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
            default:
                break;
        }
    }

    public void levelUp()
    {
        if (level < maxLevel)
        {
            level++;
            //ability.LevelUp();
        }
    }

    public float GetCurrentCooldownTime()
    {
        return cooldownTime;
    }

    public float GetCurrentActiveTime()
    {
        return activeTime;
    }

    public float GetAbilityCooldownTime()
    {
        return ability.cooldownTime;
    }

    public float GetAbilityActiveTime()
    {
        return ability.activeTime;
    }
}
