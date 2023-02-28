using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameProgress
{
    private static string level1 = "FirstLevel";
    private static string level2 = "SecondLevel";
    private static string level3 = "ThirdLevel";
    public static int currentLevel = 1;

    public static void LoadNextLevel()
    {
        if (currentLevel == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level2);
            currentLevel = 2;
        }
        else if (currentLevel == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level3);
            currentLevel = 3;
        }
        else if (currentLevel == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level1);
            currentLevel = 1;
        }
    }

    // go to current level
    public static void LoadCurrentLevel()
    {
        if (currentLevel == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level1);
        }
        else if (currentLevel == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level2);
        }
        else if (currentLevel == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level3);
        }
    }

    // Load win scene
    public static void Win()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
    }
}
