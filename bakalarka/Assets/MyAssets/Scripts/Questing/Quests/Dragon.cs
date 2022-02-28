using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Quest
{
    public void Awake()
    {
        this.questName = "Dragon";
        this.description = "Kill the beast and bring me his head.";
        this.experienceReward = 500;

        goals.Add(new CollectionGoal(this, "Dragon Head", "Bring me head of that dragon.", false, 0, 1));

        goals.ForEach(goal => goal.Init());

    }
}
