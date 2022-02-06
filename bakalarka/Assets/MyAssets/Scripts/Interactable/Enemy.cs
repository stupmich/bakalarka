using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void interact()
    {
        base.interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }

    }

    public CharacterStats GetCharacterStats()
    {
        return myStats;
    }
}
