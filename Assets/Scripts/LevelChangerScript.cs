using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour {

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");            
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
