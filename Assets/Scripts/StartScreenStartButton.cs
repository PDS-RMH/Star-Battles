using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class StartScreenStartButton : MonoBehaviour
{

    public void loadlevel()
    {
        SceneManager.LoadScene("TestGameScene");

    }

}
