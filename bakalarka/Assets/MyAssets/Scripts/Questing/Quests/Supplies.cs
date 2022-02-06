using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : Quest
{
    void Start()
    {
        this.questName = "Supplies";
        this.description = "Find out what happened with the cargo.";
        this.experienceReward = 150;

        goals.Add(new KillGoal(this, 0, "Kill 3 skeletons", false, 0, 1));
        goals.Add(new KillGoal(this, 1, "Kill necromancer", false, 0, 1));

        goals.ForEach(goal => goal.Init());

    }
}
