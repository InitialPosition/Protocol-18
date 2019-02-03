using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour {

    public string levelToLoad;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player(Clone)") {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
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
