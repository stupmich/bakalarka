using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public Loot[] lootTable;
    private int total;
    private int randomNumber;

    public void DropLoot(Vector3 position)
    {

        foreach (var item in lootTable)
        {
            total += item.dropChance;
        }

        randomNumber = Random.Range(0, total);

        foreach (var item in lootTable)
        {
            if (randomNumber <= item.dropChance)
            {
                Instantiate(item.item, position , transform.rotation);
            }
            else
            {
                randomNumber -= item.dropChance;
            }
        }
    }
}
