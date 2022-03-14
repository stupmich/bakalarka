using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        { 
            Destroy(this.gameObject);
        }
       
    }
    #endregion

    public List<Item> items = new List<Item>();
    public List<Equipment> equipedItems = new List<Equipment>();

    public int space = 20;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    void Start()
    {
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough space in inventory");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }
        return true;
    }

    public bool AddEquipIntoSlot(Equipment item)
    {
        if (!item.isDefaultItem)
        {
            equipedItems.Add(item);

            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }
        return true;
    }

    public bool RemoveEquipFromSlot(Equipment item)
    {
        equipedItems.Remove(item);
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
}
