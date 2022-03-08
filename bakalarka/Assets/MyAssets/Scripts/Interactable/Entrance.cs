using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Entrance : Interactable
{
    [SerializeField]
    private StateManager stateManager;

    public int indexOfScene;
    // Type in the name of the Scene you would like to load in the Inspector
    public string m_Scene;

    //public void Awake()
    //{
    //    stateManager = StateManager.instance;
    //}

    public override void interact()
    {
        base.interact();
        stateManager.SaveEnemiesState();
        stateManager.SavePlayerState();
        StartCoroutine(Enter());
        stateManager.LoadEnemiesState();
    }


    IEnumerator Enter()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_Scene, LoadSceneMode.Single);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
