using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private GameObject questDescription;

    private List<Quest> quests = new List<Quest>();

    [SerializeField]
    private Text questNameText;
    [SerializeField]
    private TextMeshProUGUI questDescriptionText;
    [SerializeField]
    private TextMeshProUGUI questGoalsText;
    [SerializeField]
    private Text questXPText;
    [SerializeField]
    private Text questGoldText;

    void Start()
    {
        Villager.OnQuestAssigned += OnQuestAssigned;
    }

    public void OnQuestAssigned(Quest pQuest)
    {
        GameObject questName = GameObject.Instantiate(questPrefab, new Vector3(0,0,0), Quaternion.identity, panel.transform) as GameObject;
        questName.GetComponent<Text>().text = pQuest.questName;

        questName.GetComponentInChildren<Button>().onClick.AddListener(() => ClickOnQuest(questName.GetComponent<Text>().text));
        quests.Add(pQuest);
    }

    public void ClickOnQuest(string pQuestName)
    {
        questGoalsText.text = "";

        foreach (Quest quest in quests)
        {
            if (quest.questName == pQuestName)
            {
                questDescription.SetActive(true);
                if (quest.completed)
                {
                    questNameText.text = quest.name + "(Completed)";
                }
                else
                {
                    questNameText.text = quest.name;
                }

                questDescriptionText.text = quest.description;
                foreach (Goal goal in quest.goals)
                {
                    questGoalsText.text += goal.description + " " + goal.currentAmount + "/" + goal.requiredAmount + "\n";
                    Debug.Log(goal.description);
                }
                questXPText.text = "XP: " + quest.experienceReward.ToString();

            }
        }
    }
}
