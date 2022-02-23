using UnityEngine;

public class ItemPickUp : Interactable {

    public Item item;

    public override void interact()
    {
        base.interact();
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        if (Inventory.instance.Add(item))
        {
            Destroy(gameObject);
        }  
    }
}
