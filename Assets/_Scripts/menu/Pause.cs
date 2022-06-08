using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject panel;
    int currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        panel.SetActive(false);
    }

    public void MenuPause()
    {
        panel.SetActive(true);
        Time.timeScale = 0; // 
    }

    public void MenuResume()
    {
        panel.SetActive(false);
        Time.timeScale = 1; // 
    }

    public void ExitButton()
    {
        SceneTransition.SwitchToScene("Menu");
        Time.timeScale = 1; // 
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1; // 
    }

}
