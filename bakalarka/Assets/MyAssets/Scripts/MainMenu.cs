using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup mainMenu;
    [SerializeField]
    private GameObject loadingBar;

    [SerializeField]
    private StateManager stateManager;

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject cavePosition;
    [SerializeField]
    private GameObject mainPosition;


    public bool fadeOut = false;

    public void Start()
    {
        this.mainMenu = this.gameObject.GetComponent<CanvasGroup>();
    }

    public void Update()
    {
        if (fadeOut)
        {
            if (mainMenu.alpha >= 0)
            {
                mainMenu.alpha -= Time.deltaTime * 3;
            }
        }
    }

    public void ClickOnStartNewGame()
    {
        FadeOutMainMenu();
        StartCoroutine(StartNewGame(stateManager.ResetSceneState()));
        stateManager.ResetEnemiesState();
        stateManager.ResetPlayerState();
        stateManager.ResetQuestsState();
    }

    public void ClickOnContinue()
    {
        FadeOutMainMenu();
        StartCoroutine(StartNewGame(stateManager.LoadSceneState()));
        //if (stateManager.LoadSceneState() == "Main")
        //{
        //    Instantiate(playerPrefab, mainPosition.transform.position, Quaternion.identity);
        //}
        //else if (stateManager.LoadSceneState() == "Cave")
        //{
        //    Instantiate(playerPrefab, cavePosition.transform.position, Quaternion.identity);
        //}

    }

    public void ClickOnExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator StartNewGame(string sceneName)
    {
        Image bar = this.loadingBar.GetComponent<Image>();

        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            bar.fillAmount = asyncLoad.progress;
            yield return null;
        }

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public void FadeOutMainMenu()
    {
        this.fadeOut = true;
    }
}
