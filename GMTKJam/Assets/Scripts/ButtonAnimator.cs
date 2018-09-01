using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour {

    Animator anim;

    public float baseSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetFloat("speed", baseSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
