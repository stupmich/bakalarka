using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Interactable
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField]
    private GameObject playerQuests;
    [SerializeField]
    private List<string> questTypes;
    private Quest quest { get; set; }

    public static event System.Action<Quest> OnQuestAssigned;
    public static event System.Action<Quest> OnQuestTurnedIn;

    public void Start()
    {
        DialogueManager.OnQuestChoice += TalkAboutQuest;
    }

    public override void interact()
    {
        base.interact();

        Quest[] assignedQuests = playerQuests.GetComponents<Quest>();
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
        Quest[] assignedQuests = playerQuests.GetComponents<Quest>();

        for (int i = 0; i < assignedQuests.Length; i++)
        {
            if (assignedQuests[i].questName == pQuestType)
            {
                if (CheckQuestCompletion(assignedQuests[i]))
                {
                    if (OnQuestTurnedIn != null)
                        OnQuestTurnedIn(assignedQuests[i]);
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
                AssignQuest(type);
                return;
            }
        }
 
    }

    void AssignQuest(string pQuestType)
    {
        quest = (Quest)playerQuests.AddComponent(System.Type.GetType(pQuestType));

        if (OnQuestAssigned != null)
            OnQuestAssigned(quest);
    }

    bool CheckQuestCompletion(Quest pQuest)
    {
        if (pQuest.completed)
        {
            foreach (Goal goal in pQuest.goals)
            {
                if (goal.GetType().ToString() == "CollectionGoal")
                {
                    CollectionGoal collGoal = (CollectionGoal)goal;
                    foreach (QuestItem qi in collGoal.GetPickedItems())
                    {
                        qi.TurnQuestIn();
                    }
                }
            }
            pQuest.GiveReward();
            questTypes.Remove(pQuest.questName);
            return true;
        } else
        {
            return false;
        }
    }

}
