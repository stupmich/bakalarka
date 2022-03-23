using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region Singleton
    public static PauseMenu instace;

    void Awake()
    {
        instace = this;
    }
    #endregion

    [SerializeField]
    private GameObject pauseMenuUI;
    private bool gameIsPaused = false;

    void Update()
    {
        if (Input.GetButtonDown("PauseMenu"))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
