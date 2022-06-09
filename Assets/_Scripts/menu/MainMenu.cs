using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Q;
    public GameObject Options;
    public GameObject Level;
    public GameObject con;

    private void Start()
    {
        Q.SetActive(false);
        Options.SetActive(false);
        Level.SetActive(false);
        con.SetActive(false);

    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("LevelComplete") == 3)
        {
            con.SetActive(true);
        }
    }

    public void loadLevel(string sceneName)
    {
        SceneTransition.SwitchToScene(sceneName);
    }

    public void StartButton()
    {
        Level.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void QButton()
    {
        Q.SetActive(true);
    }

    public void QBackButton()
    {
        Q.SetActive(false);
    }

    public void levelChangeBackButton()
    {
        Level.SetActive(false);
    }

    public void OptionsButton()
    {
        Options.SetActive(true);
    }

    public void OptionsBackButton()
    {
        Options.SetActive(false);
    }

    /*
    public void level1()
    {
        SceneManager.LoadScene(1);
    }

    public void level2()
    {
        SceneManager.LoadScene(2);
    }

    public void level3()
    {
        SceneManager.LoadScene(3);
    }
    */
}
