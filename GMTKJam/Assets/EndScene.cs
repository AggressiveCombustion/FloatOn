using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.instance.AddTimer(2.0f, GameManager.instance.FadeToBlack);
        GameManager.instance.AddTimer(7.0f, GameManager.instance.GoToMainMenu);
        //GameManager.instance.AddTimer(1.0f, DoAThing);
	}

    void DoAThing()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
