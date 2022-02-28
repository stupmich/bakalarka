using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : Quest
{
    public void Awake()
    {
        this.questName = "Supplies";
        this.description = "Find out what happened with the wagon.";
        this.experienceReward = 150;
        this.itemReward = (Equipment)Resources.Load("SteelHelmetEquipment");

        goals.Add(new ExploreGoal(this, "Wagon", "Find wagon with supplies.", false, 0, 1));

        goals.ForEach(goal => goal.Init());
    }
}
