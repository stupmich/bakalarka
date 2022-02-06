using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Loot")]
public class Loot : ScriptableObject
{
    public GameObject item;
    public int dropChance;


}
