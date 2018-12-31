using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene("TestGameScene");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadShipSelection()
    {
        SceneManager.LoadScene("ShipSelection");
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadIAPScene()
    {
        SceneManager.LoadScene("IAPScene");
    }

    public void LoadScoreScreen()
    {
        SceneManager.LoadScene("ScoreScreen");
    }
    public void LoadExit()
    {
        Application.Quit();
    }

}
