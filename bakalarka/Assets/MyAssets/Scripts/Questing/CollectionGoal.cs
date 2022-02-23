using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : Goal
{
    public string itemID;
    public List<QuestItem> pickedItems{ get; set; } = new List<QuestItem>();

    public CollectionGoal(Quest pQuest, string pItemID, string pDescription, bool pCompleted, int pCurrentAmount, int pRequiredAmount)
    {
        this.quest = pQuest;
        this.itemID = pItemID;
        this.description = pDescription;
        this.completed = pCompleted;
        this.currentAmount = pCurrentAmount;
        this.requiredAmount = pRequiredAmount;
    }

    public override void Init()
    {
        base.Init();
        QuestItemPickUp.OnItemPickedUp += ItemPickedUp;

    }

    void ItemPickedUp(QuestItem pItem)
    {
        if (pItem.name == itemID)
        {
            pickedItems.Add(pItem);
            this.currentAmount++;
            Debug.Log(this.currentAmount);
            CheckCompletion();
        }
    }

    public List<QuestItem> GetPickedItems()
    {
        return this.pickedItems;
    }
}
