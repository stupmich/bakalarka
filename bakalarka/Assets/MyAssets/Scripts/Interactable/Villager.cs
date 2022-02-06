using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Interactable
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool assignedQuest { get; set; }
    public bool Helped { get; set; }

    [SerializeField]
    private GameObject quests;
    [SerializeField]
    private List<string> questTypes;
    private Quest quest { get; set; }

    public void Start()
    {
        DialogueManager.OnQuestChoice += Quest;

    }

    public override void interact()
    {
        base.interact();
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);


    }

    public void Quest(string pQuestType)
    {
        foreach (string type in questTypes)
        {
            if (type == pQuestType)
            {
                Debug.Log(type);
                AssignQuest(type);
                //if (!assignedQuest && !Helped)
                //{
                //    AssignQuest();
                //}
                //else if (assignedQuest && !Helped)
                //{
                //    CheckQuestCompletion();
                //}
                //else
                //{
                //    //dialog dik bye
                //}
            }
        }
    }

    void AssignQuest(string pQuestType)
    {
        assignedQuest = true;
        quest = (Quest)quests.AddComponent(System.Type.GetType(pQuestType));

    }

    void CheckQuestCompletion()
    {
        if (quest.completed)
        {
            quest.GiveReward();
            Helped = true;
            assignedQuest = false;
            // novy dialog
        } else
        {
            //dialog nespravil si quest
        }
    }

}
