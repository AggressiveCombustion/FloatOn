using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void bStart()
    {
        GameManager.instance.AddTimer(1f, GameManager.instance.GoToLevelSelect);

        GameObject title = GameObject.Find("Title");
        if (title != null)
        {
            title.GetComponent<Animator>().SetTrigger("fall");
            GameObject.Find("bParent").GetComponent<Animator>().SetTrigger("fall");
        }

    }

    public void bOptions()
    {
        GameManager.instance.AddTimer(1.0f, GameManager.instance.GoToOptions);

        GameObject title = GameObject.Find("Title");
        if (title != null)
        {
            title.GetComponent<Animator>().SetTrigger("fall");
            GameObject.Find("bParent").GetComponent<Animator>().SetTrigger("fall");
        }
    }

    public void bExitToDesktop()
    {
        GameManager.instance.AddTimer(1.0f, GameManager.instance.ExitToDesktop);

        GameObject title = GameObject.Find("Title");
        if (title != null)
        {
            title.GetComponent<Animator>().SetTrigger("fall");
            GameObject.Find("bParent").GetComponent<Animator>().SetTrigger("fall");
        }
    }
}
