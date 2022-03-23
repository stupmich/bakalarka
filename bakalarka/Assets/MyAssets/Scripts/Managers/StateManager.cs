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
    private PlayerStats playerStats;
    [SerializeField]
    private ExperienceManager experienceManager;

    private PlayerAbilities playerAbilities;
    [SerializeField]
    private List<PlayerAbility> allAbilitiesPrefabs;

    [SerializeField]
    private GameObject assignedQuests;
    [SerializeField]
    private List<string> allQuests;
    [SerializeField]
    private List<Villager> questGivers;

    public static event System.Action<Quest> OnQuestAssigned;
    public event System.Action<PlayerAbility> OnAbilitiesLoaded;

    void Awake()
    {
        EnemyStats.ChangedState += EnemyDied;
        Villager.OnQuestTurnedIn += OnTurnedInSaveQuestState;

        this.LoadEnemiesState();

        if (this.player != null)
        {
            this.LoadPlayerState();
        }
    }

    private void Start()
    {
        if (this.player != null)
        {
            this.LoadQuestsState();
            this.LoadInventoryState();
            this.LoadPlayerAbilities();
        }
    }

    private void OnApplicationQuit()
    {
        this.SaveGameState();
    }

    public void SaveGameState()
    {
        if (this.player != null)
        {
            this.SaveEnemiesState();
            this.SavePlayerState();
            this.SaveSceneState();
            this.SaveQuestsState();
            this.SaveInventoryState();
            this.SavePlayerAbilities();
        }
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
        playerStats = PlayerStats.instance;
        

        var writer = QuickSaveWriter.Create("Player");

        if (player != null)
        {
            writer.Write("positionX", player.gameObject.transform.position.x);
            writer.Write("positionY", player.gameObject.transform.position.y);
            writer.Write("positionZ", player.gameObject.transform.position.z);

            writer.Write("vitality", playerStats.vitality.GetBaseValue());
            writer.Write("strength", playerStats.strength.GetBaseValue());
            writer.Write("dexterity", playerStats.dexterity.GetBaseValue());
            writer.Write("blockChance", playerStats.blockChance.GetBaseValue());
            writer.Write("critChance", playerStats.critChance.GetBaseValue());
            writer.Write("attackSpeed", playerStats.attackSpeed.GetBaseValue());

            writer.Write("currentXP", experienceManager.currentXP);
            writer.Write("toLevelXP", experienceManager.toLevelXP);
            writer.Write("level", experienceManager.level);
            writer.Write("statPoints", experienceManager.statPoints);
            writer.Write("abilityPoints", experienceManager.abilityPoints);

            writer.Commit();
        }
    }

    public void LoadPlayerState()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

        var reader = QuickSaveReader.Create("Player");

        if (player != null && reader.Exists("positionX") && reader.Exists("positionY") && reader.Exists("positionZ"))
        {
            reader.Read<float>("positionX", (x) => { position.x = x; });
            reader.Read<float>("positionY", (y) => { position.y = y; });
            reader.Read<float>("positionZ", (z) => { position.z = z; });

            Rigidbody rigidBody = player.GetComponent<Rigidbody>();

            if (rigidBody != null)
            {
                rigidBody.gameObject.SetActive(false);
            }
            player.transform.position = position;
            if (rigidBody != null)
            {
                rigidBody.gameObject.SetActive(true);
            }
            
            reader.Read<int>("vitality", (z) => { playerStats.vitality.SetBaseValue(z); });
            reader.Read<int>("strength", (z) => { playerStats.strength.SetBaseValue(z); });
            reader.Read<int>("dexterity", (z) => { playerStats.dexterity.SetBaseValue(z); });
            reader.Read<int>("blockChance", (z) => { playerStats.blockChance.SetBaseValue(z); });
            reader.Read<int>("critChance", (z) => { playerStats.critChance.SetBaseValue(z); });
            reader.Read<int>("attackSpeed", (z) => { playerStats.attackSpeed.SetBaseValue(z); });

            reader.Read<int>("currentXP", (z) => { experienceManager.currentXP = z; });
            reader.Read<int>("toLevelXP", (z) => { experienceManager.toLevelXP = z; });
            reader.Read<int>("level", (z) => { experienceManager.SetLevel(z); });
            reader.Read<int>("statPoints", (z) => { experienceManager.statPoints = z; });
            reader.Read<int>("abilityPoints", (z) => { experienceManager.abilityPoints = z; });
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
                }
                else
                {
                    turnedIn = false;
                }

                if (!turnedIn)
                {
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

                foreach (Villager questGiver in questGivers)
                {
                    if (questGiver.GetAvaibleQuests().Contains(name))
                    {
                        questGiver.InitDialogueOnLoad(name);
                    }
                }
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
            writer.Delete(key);
        }
        writer.Commit();
    }

    public void SaveInventoryState()
    {
        if (Inventory.instance != null)
        {
            List<Item> itemsInBag = Inventory.instance.items;
            List<Equipment> equipedItems = Inventory.instance.equipedItems;
            var writer = QuickSaveWriter.Create("ItemsBag");

            foreach (string key in writer.GetAllKeys())
            {
                writer.Delete(key);
                writer.Commit();
            }

            foreach (Item item in itemsInBag)
            {
                writer.Write(item.name, item.name.Replace(" ", ""));
                writer.Commit();
            }

            writer = QuickSaveWriter.Create("ItemsEquiped");

            foreach (string key in writer.GetAllKeys())
            {
                writer.Delete(key);
                writer.Commit();
            }

            foreach (Equipment equip in equipedItems)
            {
                writer.Write(equip.name, equip.name.Replace(" ", ""));
                writer.Commit();
            }
            
        }
    }

    public void LoadInventoryState()
    {
        if (Inventory.instance != null)
        {
            var readerItems = QuickSaveReader.Create("ItemsBag");
            Item item = null;
            Equipment equip = null;

            string itemName = "";
            string path = "";

            foreach (string key in readerItems.GetAllKeys())
            {
                readerItems.Read<string>(key, (r) => { itemName = r; });
                path = "Items/" + itemName;
                item = (Item)Resources.Load(path);

                Inventory.instance.Add(item);
            }

            var readerEquip = QuickSaveReader.Create("ItemsEquiped");

            foreach (string key in readerEquip.GetAllKeys())
            {
                readerEquip.Read<string>(key, (r) => { itemName = r; });
                path = "Items/" + itemName;
                equip = (Equipment)Resources.Load(path);
                if (!equip.isDefaultItem)
                {
                    EquipmentManager.instance.Equip(equip);
                }
            }
        }
    }

    public void ResetInventoryState()
    {
        var writerItems = QuickSaveWriter.Create("ItemsBag");

        foreach (string key in writerItems.GetAllKeys())
        {
            writerItems.Delete(key);
        }
        writerItems.Commit();

        var writerEquip = QuickSaveWriter.Create("ItemsEquiped");
        foreach (string key in writerEquip.GetAllKeys())
        {
            writerEquip.Delete(key);
        }
        writerEquip.Commit();
    }

    public void SavePlayerAbilities()
    {
        var writer = QuickSaveWriter.Create("Abilities");
        this.playerAbilities = PlayerAbilities.instance;
        
        foreach (PlayerAbility playerAbility in playerAbilities.abilities)
        {
            writer.Write(playerAbility.ability.name + " name", playerAbility.ability.name);
            writer.Write(playerAbility.ability.name + " level", playerAbility.level);
            writer.Write(playerAbility.ability.name + " key", playerAbility.key.ToString());
        }
        writer.Commit();
    }

    public void LoadPlayerAbilities()
    {
        var reader = QuickSaveReader.Create("Abilities");
        PlayerAbility pa = ScriptableObject.CreateInstance<PlayerAbility>();
        this.playerStats = PlayerStats.instance;


        foreach (PlayerAbility playerAbilityPrefab in this.allAbilitiesPrefabs)
        {
            if (reader.Exists(playerAbilityPrefab.ability.name + " name"))
            {
                pa = playerAbilityPrefab;
                reader.Read<int>(playerAbilityPrefab.ability.name + " level", (r) => { pa.level = r; });
                reader.Read<KeyCode>(playerAbilityPrefab.ability.name + " key", (r) => { pa.key = r; });
                pa.SetStats(this.playerStats);

                PlayerAbilities.instance.abilities.Add(pa);

                if (OnAbilitiesLoaded != null)
                {
                    OnAbilitiesLoaded(pa);
                }
            }
        }
    }

    public void ResetPlayerAbilities()
    {
        var writer = QuickSaveWriter.Create("Abilities");

        foreach (string key in writer.GetAllKeys())
        {
            writer.Delete(key);
        }
        writer.Commit();
    }

}
