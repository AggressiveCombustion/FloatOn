using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public Camera cam;
    public Animator anim;

	// Use this for initialization
	void Start () {

        GetComponent<SpriteRenderer>().color = new Color(cam.backgroundColor.r,
                                                         cam.backgroundColor.g,
                                                         cam.backgroundColor.b);

        anim = GetComponent<Animator>();
        anim.SetTrigger("FadeOut");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeToBlack()
    {
        anim.SetTrigger("FadeIn");
    }

    public void FadeFromBlack()
    {
        anim.SetTrigger("FadeOut");
    }
}
