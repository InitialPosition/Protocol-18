using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour {

    private GameObject introduction;
    private GameObject task;
    private GameObject titel;
    private GameObject controlScreen;
    private bool startReady = false;
    private bool gameReady = false;
    private bool stateOfControlsScreen = false; // Is true if the control screen is visible
    private float timer;

	// Use this for initialization
	void Start () {
        introduction = GameObject.Find("Introduction");
        for(int i = 0; i < introduction.transform.childCount; i++)
        {
            introduction.transform.GetChild(i).GetComponent<Text>().color = new Color(
                                                                     introduction.transform.GetChild(i).GetComponent<Text>().color.r,
                                                                     introduction.transform.GetChild(i).GetComponent<Text>().color.g,
                                                                     introduction.transform.GetChild(i).GetComponent<Text>().color.b,
                                                                     0.0f);
        }      
        task = GameObject.Find("Menu");
        titel = GameObject.Find("Titel");
        controlScreen = GameObject.Find("ControlScreen");
        controlScreen.SetActive(false);
        task.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (startReady)
        {

            introduction.SetActive(false);
            if (!gameReady)
            {
                titel.GetComponent<Text>().color = new Color(titel.GetComponent<Text>().color.r,
                                                             titel.GetComponent<Text>().color.g,
                                                             titel.GetComponent<Text>().color.b,
                                                             titel.GetComponent<Text>().color.a + Time.deltaTime * 0.2f);
                if(titel.GetComponent<Text>().color.a >= 1)
                {
                    gameReady = true;
                    task.SetActive(true);
                }
            }
            
        }else
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            for (int i = 0; i < introduction.transform.childCount; i++)
            {
                introduction.transform.GetChild(i).GetComponent<Text>().color = new Color(
                                                                         introduction.transform.GetChild(i).GetComponent<Text>().color.r,
                                                                         introduction.transform.GetChild(i).GetComponent<Text>().color.g,
                                                                         introduction.transform.GetChild(i).GetComponent<Text>().color.b,
                                                                         introduction.transform.GetChild(i).GetComponent<Text>().color.a + Time.deltaTime * 0.2f);
            }

            if (timer > 6)
            {
                startReady = true;
            }
        }

        if (Input.anyKey)
        {
            startReady = true;
            gameReady = true;
            titel.GetComponent<Text>().color = new Color(titel.GetComponent<Text>().color.r,
                                                             titel.GetComponent<Text>().color.g,
                                                             titel.GetComponent<Text>().color.b,
                                                             titel.GetComponent<Text>().color.a + 255);
            task.SetActive(true);
        }
    }

    public void ShowControls()
    {
        stateOfControlsScreen = !stateOfControlsScreen;

        foreach (Transform child in transform)
        {
            if (child.name == "ControlScreen")
            {
                child.gameObject.SetActive(stateOfControlsScreen);
            }
            else
            {
                child.gameObject.SetActive(!stateOfControlsScreen);
            }
        }
    }
}
