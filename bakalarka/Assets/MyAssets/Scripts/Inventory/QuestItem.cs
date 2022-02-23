using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestItem", menuName = "Inventory/QuestItem")]
public class QuestItem : Item
{
    public Quest quest;

    public override void Use()
    {
        base.Use();
    }

    public void TurnQuestIn()
    {
        RemoveFromInventory();
    }
}
