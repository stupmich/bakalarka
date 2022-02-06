using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationDict;

    protected override void Start()
    {
        base.Start();
//        DontDestroyOnLoad(gameObject);
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations anim in weaponAnimations)
        {
            weaponAnimationDict.Add(anim.weapon, anim.clips);
        }
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimationDict.ContainsKey(newItem))
            {
                currentAttackAnimationSet = weaponAnimationDict[newItem];
            }
        } else if (newItem == null && oldItem != null && oldItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimationSet = defaultAttackAnimationSet;
        }

        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.equipmentSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 0);
        }
    }
    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
