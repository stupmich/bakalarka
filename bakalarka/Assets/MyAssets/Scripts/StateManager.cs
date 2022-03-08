using System.Collections;
using System.Collections.Generic;
using CI.QuickSave;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private List<GameObject> deadEnemies;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject assignedQuests;
    [SerializeField]
    private List<string> allQuests;
    [SerializeField]
    private List<Villager> questGivers;
    [SerializeField]
    private Villager questGiver;

    public static event System.Action<Quest> OnQuestAssigned;

    void Awake()
    {
        EnemyStats.ChangedState += EnemyDied;
        Villager.OnQuestTurnedIn += OnTurnedInSaveQuestState;

        this.LoadEnemiesState();
        this.LoadPlayerState();
        
    }

    private void Start()
    {
        this.LoadQuestsState();
    }

    private void OnApplicationQuit()
    {
        this.SaveEnemiesState();
        this.SavePlayerState();
        this.SaveSceneState();
        SaveQuestsState();
    }

    public void SaveEnemiesState()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var writer = QuickSaveWriter.Create("Enemies");

        foreach (GameObject enemy in enemies)
        {
            writer.Write(enemy.name, enemy.GetComponent<EnemyStats>().died);
        }

        foreach (GameObject enemy in deadEnemies)
        {
            writer.Write(enemy.name, enemy.GetComponent<EnemyStats>().died);
        }

        writer.Commit();
    }

    public void LoadEnemiesState()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var reader = QuickSaveReader.Create("Enemies");

        foreach (GameObject enemy in enemies)
        {
            if (reader.Exists(enemy.name))
            {
                reader.Read<bool>(enemy.name, (r) => { enemy.GetComponent<EnemyStats>().died = r; });
            }
        }
    }

    public void ResetEnemiesState()
    {
        var writer = QuickSaveWriter.Create("Enemies");
        var keys = writer.GetAllKeys();

        foreach (var key in keys)
        {
            writer.Delete(key);
        }
        writer.Commit();
    }

    private void EnemyDied(GameObject enemy)
    {
        deadEnemies.Add(enemy); 
    }

    public void SavePlayerState()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var writer = QuickSaveWriter.Create("Player");

        if (player != null)
        {
            writer.Write("positionX", player.gameObject.transform.position.x);
            writer.Write("positionY", player.gameObject.transform.position.y);
            writer.Write("positionZ", player.gameObject.transform.position.z);
            writer.Commit();
        }
    }

    public void LoadPlayerState()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var reader = QuickSaveReader.Create("Player");
        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

        if (player != null && reader.Exists("positionX") && reader.Exists("positionY") && reader.Exists("positionZ"))
        {
            reader.Read<float>("positionX", (x) => { position.x = x; });
            reader.Read<float>("positionY", (y) => { position.y = y; });
            reader.Read<float>("positionZ", (z) => { position.z = z; });

            Rigidbody rigidBody = player.GetComponent<Rigidbody>();

            if (rigidBody != null )
            {
                rigidBody.gameObject.SetActive(false);
            }
            player.transform.position = position;
            if (rigidBody != null)
            {
                rigidBody.gameObject.SetActive(true);
            }
        }
    }

    public void ResetPlayerState()
    {
        var writer = QuickSaveWriter.Create("Player");
        var keys = writer.GetAllKeys();

        foreach (var key in keys)
        {
            writer.Delete(key);
        }
        writer.Commit();

        Debug.Log("reset");
    }


    public void SaveSceneState()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        var writer = QuickSaveWriter.Create("Scene");

        if (currentScene != null)
        {
            writer.Write("sceneName", currentScene.name);
            writer.Commit();
        }
    }

    public string LoadSceneState()
    {
        var reader = QuickSaveReader.Create("Scene");
        string sceneName = "";

        if (reader.Exists("sceneName"))
        {
            reader.Read<string>("sceneName", (r) => { sceneName = r; });
        }
        return sceneName;
    }

    public string ResetSceneState()
    {
        var writer = QuickSaveWriter.Create("Scene");

        writer.Write("sceneName", "Main");
        writer.Commit();

        return "Main";
    }

    public void SaveQuestsState()
    {
        Quest[] quests = this.assignedQuests.GetComponents<Quest>();
        var writer = QuickSaveWriter.Create("Quests");
        
        foreach (Quest quest in quests)
        {
            writer.Write(quest.questName, quest.questName);
            writer.Write(quest.questName + "Completed", quest.completed);
            foreach (Goal goal in quest.goals)
            {
                writer.Write(quest.questName + goal.description, goal.currentAmount);
            }
            writer.Commit();
        }
    }

    public void LoadQuestsState()
    {
        var reader = QuickSaveReader.Create("Quests");
        string questName = "";
        bool completed = false;
        bool turnedIn = false;
        Quest assignedQuest;

        foreach (string name in this.allQuests)
        {
            if (reader.Exists(name))
            {
                if (reader.Exists(name + "turnedIn"))
                {
                    reader.Read<bool>(name + "turnedIn", (r) => { turnedIn = r; });
                } else
                {
                    turnedIn = false;
                }
                
                if (!turnedIn)
                {
                    Debug.Log(turnedIn + name);
                    reader.Read<string>(name, (r) => { questName = r; });
                    reader.Read<bool>(name + "Completed", (r) => { completed = r; });

                    assignedQuest = (Quest)assignedQuests.AddComponent(System.Type.GetType(questName));
                    assignedQuest.completed = completed;

                    foreach (Goal goal in assignedQuest.goals)
                    {
                        reader.Read<int>(name + goal.description, (r) => { goal.currentAmount = r; });
                    }

                    if (OnQuestAssigned != null)
                        OnQuestAssigned(assignedQuest);
                }
                questGiver.InitDialogueOnLoad(name);
            }
        }
    }

    public void OnTurnedInSaveQuestState(Quest quest)
    {
        var writer = QuickSaveWriter.Create("Quests");

        writer.Write(quest.questName + "turnedIn", quest.turnedIn);
        writer.Write(quest.questName, quest.questName);

        writer.Delete(quest.questName + "Completed");
        foreach (Goal goal in quest.goals)
        {
            writer.Delete(quest.questName + goal.description);
        }
        writer.Commit();
    }

    public void ResetQuestsState()
    {
        var writer = QuickSaveWriter.Create("Quests");

        foreach (string key in writer.GetAllKeys())
        {
            Debug.Log(key);
            writer.Delete(key);
        }
        writer.Commit();
    }
}
