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
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 1;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToAct2()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 2;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToAct3()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 3;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToAct4()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 4;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToMainMenu()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 1;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.GoToMainMenu);
        //SceneManager.LoadScene("4-1");
    }
}
