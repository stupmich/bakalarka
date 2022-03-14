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
    private List<string> avaibleQuests;
    private Quest quest { get; set; }

    public static event System.Action<Quest> OnQuestAssigned;
    public static event System.Action<Quest> OnQuestTurnedIn;

    public void Awake()
    {
        DialogueManager.OnQuestChoice += TalkAboutQuest;
    }

    public override void interact()
    {
        base.interact();

        Quest[] assignedQuests = playerQuests.GetComponents<Quest>();

        DialogueManager.GetInstance().SetCurrentStory(inkJSON);
        
        foreach (string type in avaibleQuests)
        {
            for (int i = 0; i < assignedQuests.Length; i++)
            {
                if (assignedQuests[i].completed == true)
                {
                    DialogueManager.GetInstance().SetDialogueBoolVariable(assignedQuests[i].questName, true);
                }
            }
        }
        DialogueManager.GetInstance().EnterDialogueMode();
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
                    assignedQuests[i].turnedIn = true;
                    if (OnQuestTurnedIn != null)
                        OnQuestTurnedIn(assignedQuests[i]);
                    Object.Destroy(assignedQuests[i]);
                    DialogueManager.GetInstance().SetDialogueBoolVariable(assignedQuests[i].questName, false);
                }
                return;
            }
        }

        foreach (string type in avaibleQuests)
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
            avaibleQuests.Remove(pQuest.questName);
            return true;
        } else
        {
            return false;
        }
    }

    public void InitDialogueOnLoad(string variable)
    {
        DialogueManager.GetInstance().SetCurrentStory(this.inkJSON);
        DialogueManager.GetInstance().SetDialogueBoolVariable(variable.ToLower() + "Var", false);
    }

    public List<string> GetAvaibleQuests()
    {
        return this.avaibleQuests;
    }
}
