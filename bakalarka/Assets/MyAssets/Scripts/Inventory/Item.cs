using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; // new pretoze item uz ma name pouzivame toto nove
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use ()
    {
        Debug.Log("Using" + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
