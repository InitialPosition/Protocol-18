using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {
	private GameObject crosshair, backBtn, exitBtn, ctrlText;
	void Start () {
		crosshair = GameObject.Find("Crosshair");
		backBtn = GameObject.Find("BackBtn");
		exitBtn = GameObject.Find("ExitBtn");
		ctrlText = GameObject.Find("ControlTxt");

		backToGame();
	}

	public void openPauseMenu() {
		crosshair.SetActive(false);
		backBtn.SetActive(true);
		exitBtn.SetActive(true);
		ctrlText.SetActive(true);
	}

	public void backToGame() {
		Cursor.lockState = CursorLockMode.Locked;

		crosshair.SetActive(true);
		backBtn.SetActive(false);
		exitBtn.SetActive(false);
		ctrlText.SetActive(false);
	}

	public void backToMenu() {
		SceneManager.LoadScene("MainMenu");
	}
}
