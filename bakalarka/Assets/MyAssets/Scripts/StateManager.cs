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

    void Awake()
    {
        EnemyStats.ChangedState += EnemyDied;
        this.LoadEnemiesState();
        this.LoadPlayerState();

        Debug.Log("START");

    }

    private void OnApplicationQuit()
    {
        this.SaveEnemiesState();
        this.SavePlayerState();
        this.SaveSceneState();
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
        Debug.Log("load");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var reader = QuickSaveReader.Create("Enemies");

        Debug.Log("ENEMIES  " + enemies);
        foreach (GameObject enemy in enemies)
        {
            if (reader.Exists(enemy.name))
            {
                reader.Read<bool>(enemy.name, (r) => { enemy.GetComponent<EnemyStats>().died = r; });
                Debug.Log("Load" + enemy + enemy.GetComponent<EnemyStats>().died);
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
            //if exists
            reader.Read<float>("positionX", (x) => { position.x = x; });
            reader.Read<float>("positionY", (y) => { position.y = y; });
            reader.Read<float>("positionZ", (z) => { position.z = z; });

            Rigidbody rigidBody = player.GetComponent<Rigidbody>();

            if (rigidBody != null )
            {
                Debug.Log(rigidBody);
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


}
