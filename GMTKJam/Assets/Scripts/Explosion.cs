using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public AudioClip clipPlayer;
    public AudioClip clipEnemy;

    public bool player = false;

	// Use this for initialization
	void Start () {
        GameManager.instance.AddShake(0.75f, "Main Camera", 2f);

        if (player)
            GetComponent<AudioSource>().clip = clipPlayer;
        else
            GetComponent<AudioSource>().clip = clipEnemy;

        GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FinishAnim()
    {
        Destroy(gameObject);
    }
}
