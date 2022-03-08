using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField]
    private GameObject questLogUI;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private GameObject questDescription;
    [SerializeField]
    private GameObject noQuestsActive;

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
    private GameObject itemReward;

    void Awake()
    {
        Villager.OnQuestAssigned += OnQuestAssigned;
        Villager.OnQuestTurnedIn += OnQuestTurnedIn;

        StateManager.OnQuestAssigned += OnQuestAssigned;

        noQuestsActive.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("QuestLog"))
        {
            questLogUI.SetActive(!questLogUI.activeSelf);
            if (questDescription.activeSelf)
            {
                questDescription.SetActive(false);
                if (itemReward.GetComponent<ItemRewardUI>().GetItemInfo() != null)
                {
                    Object.Destroy(itemReward.GetComponent<ItemRewardUI>().GetItemInfo());
                }
            }
        }
    }

    public void OnQuestAssigned(Quest pQuest)
    {
        GameObject questName = GameObject.Instantiate(questPrefab, new Vector3(0,0,0), Quaternion.identity, content.transform) as GameObject;
        questName.GetComponent<Text>().text = pQuest.questName;

        questName.GetComponentInChildren<Button>().onClick.AddListener(() => ClickOnQuest(questName.GetComponent<Text>().text));
        quests.Add(pQuest);

        if (quests.Count != 0)
        {
            noQuestsActive.SetActive(false);
        }
    }

    public void OnQuestTurnedIn(Quest pQuest)
    {
        foreach (Quest quest in quests)
        {
            if (quest.questName == pQuest.questName)
            {
                foreach (Text questText in content.GetComponentsInChildren<Text>())
                {
                    if (quest.questName == questText.text)
                    {
                        Debug.Log(quest.questName + " " + questText.text);
                        Object.Destroy(questText.gameObject);
                        break;
                    }
                }
                quests.Remove(quest);
                break;
            }
        }

        if (quests.Count == 0)
        {
            noQuestsActive.SetActive(true);
        }
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
                    questNameText.text = quest.questName + "(Completed)";
                }
                else
                {
                    questNameText.text = quest.questName;
                }
                
                questDescriptionText.text = quest.description;
                foreach (Goal goal in quest.goals)
                {
                    questGoalsText.text += goal.description + " " + goal.currentAmount + "/" + goal.requiredAmount + "\n";
                }
                questXPText.text = "XP: " + quest.experienceReward.ToString();

                if (quest.itemReward != null)
                {
                    this.itemReward.GetComponent<ItemRewardUI>().SetItem(quest.itemReward);
                    this.itemReward.SetActive(true);
                    this.itemReward.GetComponent<Image>().sprite = quest.itemReward.icon;
                } else
                {
                    this.itemReward.SetActive(false);
                    this.itemReward.GetComponent<Image>().sprite = null;
                }
            }
        }
    }
}
