using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour {

    public AudioClip[] bubbleSounds;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource  = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToAct1()
    {

        /*GameObject.Find("Player In Menu").transform.position = new Vector3(
            GameObject.Find("Player In Menu").transform.position.x,
            GameObject.Find("Player In Menu").transform.position.y,
            10);*/

        audioSource.clip = bubbleSounds[Random.Range(0, 2)];
        audioSource.Play();
        

        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 1;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToAct2()
    {
        /*GameObject.Find("Player In Menu").transform.position = new Vector3(
                       GameObject.Find("Player In Menu").transform.position.x,
                       GameObject.Find("Player In Menu").transform.position.y,
                       10);*/

        audioSource.clip = bubbleSounds[Random.Range(0, 2)];
        audioSource.Play();

        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 2;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToAct3()
    {

        audioSource.clip = bubbleSounds[Random.Range(0, 2)];
        audioSource.Play();


        /*GameObject.Find("Player In Menu").transform.position = new Vector3(
               GameObject.Find("Player In Menu").transform.position.x,
               GameObject.Find("Player In Menu").transform.position.y,
               10);*/

        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 3;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToAct4()
    {

        audioSource.clip = bubbleSounds[Random.Range(0, 2)];
        audioSource.Play();

        /*GameObject.Find("Player In Menu").transform.position = new Vector3(
            GameObject.Find("Player In Menu").transform.position.x,
            GameObject.Find("Player In Menu").transform.position.y,
            10);*/

        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 4;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.StartAct);
        //SceneManager.LoadScene("4-1");
    }

    public void GoToMainMenu()
    {

        audioSource.clip = bubbleSounds[Random.Range(0, 2)];
        audioSource.Play();

        /*GameObject.Find("Player In Menu").transform.position = new Vector3(
            GameObject.Find("Player In Menu").transform.position.x,
            GameObject.Find("Player In Menu").transform.position.y,
            10);*/

        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("back");
        GameManager.instance.act = 1;
        GameManager.instance.scene = 1;
        GameManager.instance.AddTimer(1.0f, GameManager.instance.GoToMainMenu);
        //SceneManager.LoadScene("4-1");
    }
}
