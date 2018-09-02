using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belch : MonoBehaviour {

    public float duration = 0.75f;
    public float elapsed = 0;

    public AudioClip[] burpSounds;

	// Use this for initialization
	void Start () {

        GetComponent<AudioSource>().clip = burpSounds[Random.Range(0, 2)];
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        elapsed += Time.deltaTime;
        if (elapsed > duration)
            Destroy(gameObject);
	}
}
