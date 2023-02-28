using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    enum GameState { Start, Playing, Paused, End };
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject EndMenu;
    [SerializeField] List<GameObject> Cameras;
    [SerializeField] List<GameObject> ResultsText;
    [SerializeField] SizeManager size;
    GameState currentState;
    float timeSpent;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Start;
        StartMenu.SetActive(true);
        PauseMenu.SetActive(false);
        EndMenu.SetActive(false);
        ToggleCameras(false);

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentState == GameState.Playing)
        {
            timeSpent += Time.deltaTime;
        }

        if (currentState == GameState.Start)
        {
            if(Input.GetKeyDown(KeyCode.Return)) StartGame();

            if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
        }

        if (currentState == GameState.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Backspace)) ResetScene();
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
        timeSpent = 0;
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
        Time.timeScale = 0;
        ToggleCameras(false);
        EndMenu.SetActive(true);
        float percentage = size.ScalePercent;
        int mins = (int)(timeSpent / 60);
        int secs = (int)timeSpent % 60;
        ResultsText[0].GetComponent<TextMeshPro>().text = mins + "m " + secs + "s";
        string sizeAssement = "";
        string overalAssement = "";
        if(percentage > .75)
        {
            sizeAssement = "Macro";
            overalAssement = "Brash";
        }
        if (percentage <= .75 && percentage > .50)
        {
            sizeAssement = "Small";
            overalAssement = "Busy";
        }
        if (percentage <= .50 && percentage > .25)
        {
            sizeAssement = "Tiny";
            overalAssement = "Calm";
        }
        if(percentage <= .25)
        {
            sizeAssement = "Mirco";
            overalAssement = "Mindfulness";
        }
        ResultsText[1].GetComponent<TextMeshPro>().text = sizeAssement;
        ResultsText[0].GetComponent<TextMeshPro>().text = overalAssement;

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
