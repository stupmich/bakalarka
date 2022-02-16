using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : MonoBehaviour
{
    public PlayerStats playerStats;

    void Start()
    {
        playerStats = PlayerStats.instance;
        InvokeRepeating("StartTakingDamage", 0f, 1f);
    }

    void StartTakingDamage()
    {
        playerStats.TakeDamage(2);
    }
}
