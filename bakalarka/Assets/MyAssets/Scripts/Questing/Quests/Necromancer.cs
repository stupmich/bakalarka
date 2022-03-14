using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Quest
{
    // Start is called before the first frame update
    public void Awake()
    {
        this.questName = "Necromancer";
        this.description = "Kill the necromancer and the skeletons he made alive.";
        this.experienceReward = 150;
        this.itemReward = (Equipment)Resources.Load("Items/SteelLegplates");
        this.turnedIn = false;

        goals.Add(new KillGoal(this,0, "Kill 3 skeletons", false, 0, 1));
        goals.Add(new KillGoal(this,1, "Kill necromancer", false, 0, 1));

        goals.ForEach(goal => goal.Init());
    }
}
