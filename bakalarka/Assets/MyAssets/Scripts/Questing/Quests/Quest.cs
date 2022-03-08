using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    [SerializeField]
    public List<Goal> goals { get; set; } = new List<Goal>();
    public string questName;
    public string description;
    public int experienceReward;
    public Equipment itemReward;
    public bool completed;
    public bool turnedIn;

    public void CheckGoalsCompletion()
    {
        completed = goals.All(goal => goal.completed); //if finds one goal which is false then returns false
    }

    public void GiveReward()
    {
        if (itemReward != null)
        {
            Inventory.instance.Add(itemReward);
        }

        ExperienceManager.instance.AddXP(experienceReward);
    }
}
