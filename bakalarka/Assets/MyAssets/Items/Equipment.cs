using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;
    public int vitalityModifier;
    public int strengthModifier;
    public int dexterityModifier;
    public int blockChanceModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        RemoveFromInventory();
        EquipmentManager.instance.Equip(this);
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}
public enum EquipmentMeshRegion { Legs, Arms, Torso }; //rovnake ako body blandshapes v body objecte