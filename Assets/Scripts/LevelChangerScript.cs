using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour {

    public static void LoadFirstLevel()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");            
    }
}
