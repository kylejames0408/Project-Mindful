using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using Newtonsoft.Json;
using Unity.VisualScripting;
using static UnityEditor.Progress;

public class UIManager : MonoBehaviour
{
    enum GameState { Start, Playing, Paused, End };
    enum MenuSelect { Play,Quit,Volume}
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject EndMenu;
    [SerializeField] List<GameObject> Cameras;
    [SerializeField] List<GameObject> ResultsText;
    [SerializeField] SizeManager size;
    [SerializeField] List<GameObject> RightText;
    [SerializeField] List<GameObject> LeftText;
    [SerializeField] List<GameObject> VolumeText;
    [SerializeField] List<GameObject> VolumeSliders;
    public bool writeJSON = true;
    
    GameState currentState;
    MenuSelect selectedButton;

    float timeSpent;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Start;
        MenuButtonSelect(MenuSelect.Play);
        StartMenu.SetActive(true);
        PauseMenu.SetActive(false);
        EndMenu.SetActive(false);
        ToggleCameras(false);


        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // State Logic
        if (currentState == GameState.Playing)
        {
            timeSpent += Time.deltaTime;
        }

        if (currentState == GameState.Start)
        {
            if(Input.GetKeyDown(KeyCode.Return) && selectedButton == MenuSelect.Play) StartGame();

            if (Input.GetKeyDown(KeyCode.Escape) || ((Input.GetKeyDown(KeyCode.Return) && selectedButton == MenuSelect.Quit))) QuitGame();
        }

        if (currentState == GameState.End)
        {
            if (Input.GetKeyDown(KeyCode.Return)) ResetScene(); // Placeholder while ui clicking doesn't work
        }


        if (currentState == GameState.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || ((Input.GetKeyDown(KeyCode.Return) && selectedButton == MenuSelect.Quit))) ResetScene();
            if ((Input.GetKeyDown(KeyCode.Return) && selectedButton == MenuSelect.Play)) ResetScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }


        // Select Menu Buttons
        if (currentState != GameState.End && currentState != GameState.Playing) 
        { 
            // Button Select
            if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.A)))
            {
                if (selectedButton != MenuSelect.Volume) { MenuButtonSelect(MenuSelect.Play); }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectedButton != MenuSelect.Volume) { MenuButtonSelect(MenuSelect.Quit); }

            }
            if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W))) MenuButtonSelect(MenuSelect.Volume);
            if ((Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.S))) MenuButtonSelect(MenuSelect.Play);

            float time = Time.deltaTime;
            if (selectedButton == MenuSelect.Volume)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    VolumeSliders.ForEach(slider => { slider.GetComponent<Slider>().value += 0.005f; });
                }
                if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A)))
                {
                    VolumeSliders.ForEach(slider => { slider.GetComponent<Slider>().value -= 0.005f; });
                }
            }
        }
    }

    private void MenuButtonSelect(MenuSelect stateToSelect)
    {
        selectedButton = stateToSelect;
        
        LeftText.ForEach(text => { text.GetComponent<TextMeshProUGUI>().color = Color.black;
            text.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;
        });
        RightText.ForEach(text => { text.GetComponent<TextMeshProUGUI>().color = Color.black;
            text.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;
        });
        VolumeText.ForEach(text => { text.GetComponent<TextMeshProUGUI>().color = Color.black;
            text.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Normal;
        });
        
        switch (selectedButton)
        {
            case MenuSelect.Play:
                LeftText.ForEach(text => { text.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                    text.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Underline;
                });
                break;
            case MenuSelect.Quit:
                RightText.ForEach(text => { text.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                    text.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Underline;
                });
                break;
            case MenuSelect.Volume:
                VolumeText.ForEach(text => { text.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                    text.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Underline;
                });
                break;
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
                MenuButtonSelect(MenuSelect.Play);
                break;

            // Unpause the game
            case GameState.Paused:
                currentState = GameState.Playing;
                PauseMenu.SetActive(false);
                ToggleCameras(true);
                Time.timeScale = 1;
                MenuButtonSelect(MenuSelect.Play);
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
        MenuButtonSelect(MenuSelect.Play);

        Time.timeScale = 0;
        ToggleCameras(false);
        EndMenu.SetActive(true);
        float percentage = size.ScalePercent;
        int mins = (int)(timeSpent / 60);
        int secs = (int)timeSpent % 60;
        string formatedTime = mins + "m " + secs + "s";
        ResultsText[0].GetComponent<TextMeshProUGUI>().text = formatedTime;
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
            overalAssement = "Mindful";
        }



        if (timeSpent>240.0f)
        {
            overalAssement = overalAssement + " and Observant";
        }
        if(timeSpent <= 240 && timeSpent > 150)
        {
            overalAssement = overalAssement + " and Relaxed";
        }
        if (timeSpent <= 150 && timeSpent > 60)
        {
            overalAssement = overalAssement + " and Hasty";
        }
        if (timeSpent <= 60)
        {
            overalAssement = overalAssement + " and in a Hurry";
        }


        ResultsText[1].GetComponent<TextMeshProUGUI>().text = sizeAssement;
        ResultsText[2].GetComponent<TextMeshProUGUI>().text = overalAssement;
        if(writeJSON) WriteToJSON(formatedTime,percentage,overalAssement);

    }

  

    private void ToggleCameras(bool enable)
    {
        foreach (GameObject c in Cameras)
        {
            c.SetActive(enable);
        }
        //Cursor.visible = enable;
    }

    private class MindfulnessAssement
    {
        public string TimeSpent  { get; set; }
        public float SizePercent { get; set; }
        public string OverallAssesment { get; set; }
    }

    private void WriteToJSON(string timeSpent, float sizePercent, string overallAssesment)    
    {
        List<MindfulnessAssement> assements = new List<MindfulnessAssement>();

        // Reading JSON From: https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
        // Written by, L.B: https://stackoverflow.com/users/932418/l-b
        // Last Edited by, Robert Christopher: https://stackoverflow.com/users/3886729/robert-christopher

        using (StreamReader r = new StreamReader(@"MindfulData.json"))
        {
            string readingJson = r.ReadToEnd();
            var checkJSon = JsonConvert.DeserializeObject<List<MindfulnessAssement>>(readingJson);
            if (checkJSon != null) assements.AddRange(JsonConvert.DeserializeObject<List<MindfulnessAssement>>(readingJson));

            
        }

        assements.Add(new MindfulnessAssement()
        {
            TimeSpent = timeSpent,
            SizePercent = sizePercent,
            OverallAssesment = overallAssesment
        });

        // Writing to JSON: https://stackoverflow.com/questions/16921652/how-to-write-a-json-file-in-c
        // Written by, Liam: https://stackoverflow.com/users/542251/liam
        // Last Edited by, Kacper Wyczawski: https://stackoverflow.com/users/18682712/kacper-wyczawski
        string json = JsonConvert.SerializeObject(assements);

        System.IO.File.WriteAllText(@"MindfulData.json", json);
    }
}
