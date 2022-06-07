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
        Time.timeScale = 0; // bad(
    }

    public void MenuResume()
    {
        panel.SetActive(false);
        Time.timeScale = 1; // bad(
    }

    public void ExitButton()
    {
        SceneTransition.SwitchToScene("Menu");
        Time.timeScale = 1; // bad(
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1; // bad(
    }

}
