using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    enum GameState { Start, Playing, Paused, End };
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject StartMenu;
    [SerializeField] List<GameObject> Cameras;
    GameState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Start;
        StartMenu.SetActive(true);
        ToggleCameras(false);

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.Start)
        {
            if(Input.GetKeyDown(KeyCode.Return)) StartGame();

            if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        switch (currentState)
        {
            // Pause the game
            case GameState.Playing:
                currentState = GameState.Paused;
                PauseMenu.SetActive(true);
                ToggleCameras(false);
                Time.timeScale = 0;
                break;

            // Unpause the game
            case GameState.Paused:
                currentState = GameState.Playing;
                PauseMenu.SetActive(false);
                ToggleCameras(true);
                Time.timeScale = 1;
                break;

            // Do nothing
            case GameState.Start:
                break;
            case GameState.End:
                break;
        }

  
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        currentState = GameState.Playing;
        StartMenu.SetActive(false);
        ToggleCameras(true);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }

    public void EndScene()
    {
        currentState = GameState.End;
        ToggleCameras(false);
    }

    private void ToggleCameras(bool enable)
    {
        foreach (GameObject c in Cameras)
        {
            c.SetActive(enable);
        }
        //Cursor.visible = enable;
    }
}
