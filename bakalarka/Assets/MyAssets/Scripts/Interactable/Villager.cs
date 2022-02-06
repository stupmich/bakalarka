using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Interactable
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public override void interact()
    {
        base.interact();
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
