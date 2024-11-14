using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Home()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
        Time.timeScale = 1;
    }
}
