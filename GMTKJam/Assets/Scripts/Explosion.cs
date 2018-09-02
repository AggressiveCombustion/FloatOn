using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.instance.AddShake(0.75f, "Main Camera", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FinishAnim()
    {
        Destroy(gameObject);
    }
}
