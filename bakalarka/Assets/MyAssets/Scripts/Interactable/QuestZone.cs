using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestZone : Interactable
{
    public GameObject zone;
    public static event System.Action <GameObject> OnZoneInteraction;

    public override void interact()
    {
        base.interact();
        if (OnZoneInteraction != null)
            OnZoneInteraction(zone);
    }
}
