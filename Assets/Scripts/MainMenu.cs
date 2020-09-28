using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Data;

public class MainMenu : MonoBehaviour
{
    public Text highscore;
    bool isThicc=false;
    public GameObject postFx;
    public GameObject thiccMode;
    Score scoreObject = new Score();
    Settings settingsObject = new Settings();

    private void Update() 
    {
        //settings
        if(settingsObject.Performance=="true")
        {
            postFx.SetActive(false);
        }
        if(settingsObject.Quality=="true")
        {
            postFx.SetActive(true);
        }
        if(settingsObject.Thicc=="True")
        {
            thiccMode.SetActive(true);
        }
        if(settingsObject.Thicc=="False")
        {
            thiccMode.SetActive(false);
        }
    }
    private void Start() 
    {
        Time.timeScale = 1f;
        highscore.text="High Score: " + Mathf.Round(scoreObject.HighScore);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Performance()
    {
        settingsObject.Performance="true";
        settingsObject.Quality="false";
    }
    public void Quality()
    {
        settingsObject.Quality="true";
        settingsObject.Performance="false";
    }
    public void Thicc()
    {
        isThicc=!isThicc;
        settingsObject.Thicc=isThicc.ToString();
    }
}
