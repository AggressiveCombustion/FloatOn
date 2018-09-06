using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject menu;
    public bool menuOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        menu.SetActive(menuOn);

        if(Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Return))
        {
            menuOn = !menuOn;
        }

        if (menuOn)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
	}

    public void pResume()
    {
        menuOn = false;
    }

    public void pRestart()
    {
        menuOn = false;
        GameManager.instance.FadeToBlack();
        GameManager.instance.AddTimer(2.0f, GameManager.instance.RestartLevel);
    }

    public void pMainMenu()
    {
        menuOn = false;
        GameManager.instance.FadeToBlack();
        GameManager.instance.AddTimer(2.0f, GameManager.instance.GoToMainMenu);
    }
}
