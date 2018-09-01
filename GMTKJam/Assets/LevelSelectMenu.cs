using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToAct1()
    {
        GameManager.instance.act = 1;
        GameManager.instance.scene = 1;
        SceneManager.LoadScene("1-1");
    }

    public void GoToAct2()
    {
        GameManager.instance.act = 2;
        GameManager.instance.scene = 1;
        SceneManager.LoadScene("2-1");
    }

    public void GoToAct3()
    {
        GameManager.instance.act = 3;
        GameManager.instance.scene = 1;
        SceneManager.LoadScene("3-1");
    }

    public void GoToAct4()
    {
        GameManager.instance.act = 4;
        GameManager.instance.scene = 1;
        SceneManager.LoadScene("4-1");
    }

    public void GoToMainMenu()
    {
        GameManager.instance.act = 1;
        GameManager.instance.scene = 1;
        SceneManager.LoadScene("Start");
    }
}
