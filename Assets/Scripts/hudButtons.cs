using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Data;

public class hudButtons : MonoBehaviour
{
    public GameObject pauseMenu;
    public Animator pauseAnimation;
    Score scoreObject = new Score();
    public Text highscore;
    private void Update() 
    {
        highscore.text="High Score: " + Mathf.Round(scoreObject.HighScore);
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseAnimation.SetBool("isPaused", true);
        Time.timeScale=0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseAnimation.SetBool("isPaused", false);
        Time.timeScale=1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }
}
