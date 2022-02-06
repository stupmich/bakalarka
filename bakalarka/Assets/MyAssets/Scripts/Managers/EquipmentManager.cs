using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

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

    public Transform sword;
    public Transform shield;

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    public Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged; //callback method

    Inventory inventory;

    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        inventory = Inventory.instance;
        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;   
        Equipment oldItem = Unequip(slotIndex); 
       
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmetBlendShapes(newItem, 100);

        //insert itemu do slotu
        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        currentMeshes[slotIndex] = newMesh;

        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            newMesh.rootBone = sword.transform;
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
        }
        else if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Shield)
        {
            newMesh.rootBone = shield.transform;
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
        }
        else
        {
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
        }

        inventory.AddEquipIntoSlot(newItem);
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmetBlendShapes(oldItem, 0); // rozsiri postavu na miestach ktore boli pod armorom

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            if (inventory.Add(oldItem))
            {
                currentEquipment[slotIndex] = null;
                inventory.RemoveEquipFromSlot(oldItem);
            }

            return oldItem;
        }
        return null;
    }

    public Equipment UnequipByItem(Equipment item)
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            if (currentEquipment[i] == item)
            {
                Destroy(currentMeshes[i].gameObject);

                Equipment oldItem = currentEquipment[i];
                SetEquipmetBlendShapes(oldItem, 0); // rozsiri postavu na miestach ktore boli pod armorom

                if (onEquipmentChanged != null)
                {
                    onEquipmentChanged.Invoke(null, oldItem);
                    Debug.Log(item);
                }

                if (inventory.Add(oldItem))
                {
                    currentEquipment[i] = null;
                    inventory.RemoveEquipFromSlot(oldItem);
                }

                EquipAppropriateDefaultItem(oldItem.equipmentSlot);
                return oldItem;
            }
        }

        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmetBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    void EquipAppropriateDefaultItem(EquipmentSlot equipmentSlot)
    {
        foreach (Equipment item in defaultItems)
        {
            if (item.equipmentSlot == equipmentSlot)
            {
                Equip(item);
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
