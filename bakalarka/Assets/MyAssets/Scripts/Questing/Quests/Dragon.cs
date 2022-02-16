using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Quest
{
    void Start()
    {
        this.questName = "Dragon";
        this.description = "Kill the beast and bring me his head.";
        this.experienceReward = 500;

        goals.Add(new KillGoal(this, 1, "Kill dragon", false, 0, 1));

        goals.ForEach(goal => goal.Init());

    }
}
