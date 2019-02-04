using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

	private GameObject crosshair, backBtn, exitBtn, ctrlText;

	void Start () {
		crosshair = GameObject.Find("Crosshair");
		backBtn = GameObject.Find("BackBtn");
		exitBtn = GameObject.Find("ExtBtn");
		ctrlText = GameObject.Find("ControlTxt");

        backBtn.SetActive(false);
        exitBtn.SetActive(false);
        ctrlText.SetActive(false);
    }

	public void openPauseMenu() {
		crosshair.SetActive(false);
		backBtn.SetActive(true);
		exitBtn.SetActive(true);
		ctrlText.SetActive(true);
        Camera.main.GetComponent<CameraLookAroundController>().mouseUpdating = false;
	}

	public void backToGame() {
		Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("backToGame called");
		crosshair.SetActive(true);
		backBtn.SetActive(false);
		exitBtn.SetActive(false);
		ctrlText.SetActive(false);
        Camera.main.GetComponent<CameraLookAroundController>().mouseUpdating = true;
    }

	public void backToMenu() {
		SceneManager.LoadScene("MainMenu");
	}

    public bool MenuVisible()
    {
        if (backBtn.activeSelf)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
