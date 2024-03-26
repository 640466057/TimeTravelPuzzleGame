using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelecter : MonoBehaviour
{
    public void GoToLevel0()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
