using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    public void LoadTestScene()
    {
        SceneManager.LoadScene("TestGameScene");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadExit()
    {
        Application.Quit();
    }

}
