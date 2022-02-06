using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private LootTable lootTable;
    private Vector3 position;
    private bool opened;
    protected Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        lootTable = GetComponent<LootTable>();
        position = transform.GetChild(3).position;
    }

    public override void interact()
    {
        base.interact();
        Debug.Log("chest");

        if (lootTable != null && opened == false)
        {
            lootTable.DropLoot(position);
            opened = true;
            animator.SetTrigger("Open");
        }
        
    }
}
