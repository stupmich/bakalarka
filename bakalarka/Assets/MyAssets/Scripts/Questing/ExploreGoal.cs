using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreGoal : Goal
{
    public string zoneName;

    public ExploreGoal(Quest pQuest, string pZoneName, string pDescription, bool pCompleted, int pCurrentAmount, int pRequiredAmount)
    {
        this.quest = pQuest;
        this.zoneName = pZoneName;
        this.description = pDescription;
        this.completed = pCompleted;
        this.currentAmount = pCurrentAmount;
        this.requiredAmount = pRequiredAmount;
    }

    public override void Init()
    {
        base.Init();
        QuestZone.OnZoneInteraction += ZoneInteraction;
    }

    void ZoneInteraction(GameObject pZone)
    {
        if (pZone.name == zoneName)
        {
            this.currentAmount++;
            CheckCompletion();
        }
    }
}
