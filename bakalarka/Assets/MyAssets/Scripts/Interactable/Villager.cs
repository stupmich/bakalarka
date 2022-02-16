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
        DialogueManager.OnQuestChoice += TalkAboutQuest;
    }

    public override void interact()
    {
        base.interact();

        Quest[] assignedQuests = quests.GetComponents<Quest>();
        foreach (string type in questTypes)
        {
            for (int i = 0; i < assignedQuests.Length; i++)
            {
                if (assignedQuests[i].completed == true)
                {
                    DialogueManager.GetInstance().SetDialogueBoolVariable(assignedQuests[i].questName, true);
                }
            }
        }

        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    public void TalkAboutQuest(string pQuestType)
    {
        Quest[] assignedQuests = quests.GetComponents<Quest>();

        for (int i = 0; i < assignedQuests.Length; i++)
        {
            if (assignedQuests[i].questName == pQuestType)
            {
                if (CheckQuestCompletion(assignedQuests[i]))
                {
                    Object.Destroy(assignedQuests[i]);
                    DialogueManager.GetInstance().SetDialogueBoolVariable(assignedQuests[i].questName, false);
                }
                return;
            }
        }

        foreach (string type in questTypes)
        {
            if (type == pQuestType)
            {
                Debug.Log("assign " + type);
                AssignQuest(type);
                return;
            }
        }
 
    }

    void AssignQuest(string pQuestType)
    {
        assignedQuest = true;
        quest = (Quest)quests.AddComponent(System.Type.GetType(pQuestType));

    }

    bool CheckQuestCompletion(Quest pQuest)
    {
        if (pQuest.completed)
        {
            pQuest.GiveReward();
            questTypes.Remove(pQuest.questName);
            return true;
        } else
        {
            return false;
        }
    }

}
