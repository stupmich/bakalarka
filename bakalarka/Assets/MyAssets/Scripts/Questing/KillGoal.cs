using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public int enemyID;

    public KillGoal(Quest pQuest, int pEnemyID, string pDescription, bool pCompleted, int pCurrentAmount, int pRequiredAmount )
    {
        this.quest = pQuest;
        this.enemyID = pEnemyID;
        this.description = pDescription;
        this.completed = pCompleted;
        this.currentAmount = pCurrentAmount;
        this.requiredAmount = pRequiredAmount;
    }

    public override void Init()
    {
        base.Init();
        EnemyStats.OnEnemyDeath += EnemyKilled;

    }

    void EnemyKilled(int pEnemyID)
    {
        if (pEnemyID == enemyID)
        {
            this.currentAmount++;
            CheckCompletion();
        }
    }
}
