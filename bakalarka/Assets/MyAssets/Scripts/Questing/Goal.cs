using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal 
{
    public Quest quest;
    public string description;
    public bool completed;
    public int currentAmount;
    public int requiredAmount;

    public virtual void Init()
    {
        // default init 
    }

    public void CheckCompletion()
    {
        if (currentAmount >= requiredAmount)
        {
            completed = true;
        }

        quest.CheckGoalsCompletion();
    }
}
