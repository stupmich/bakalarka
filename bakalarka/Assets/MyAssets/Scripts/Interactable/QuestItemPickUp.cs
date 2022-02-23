using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemPickUp : Interactable
{
    public QuestItem item;
    public static event System.Action<QuestItem> OnItemPickedUp;

    public override void interact()
    {
        base.interact();
        Debug.Log(base.radius);
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        if (Inventory.instance.Add(item))
        {
            if (OnItemPickedUp != null)
                OnItemPickedUp(item);
            Destroy(gameObject);
        }
    }
}
