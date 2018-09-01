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
        GameManager.instance.AddTimer(2.0f, GameManager.instance.GoToLevelSelect);
    }

    public void bOptions()
    {
        GameManager.instance.AddTimer(2.0f, GameManager.instance.GoToOptions);
    }

    public void bExitToDesktop()
    {
        GameManager.instance.AddTimer(2.0f, GameManager.instance.ExitToDesktop);
    }
}
