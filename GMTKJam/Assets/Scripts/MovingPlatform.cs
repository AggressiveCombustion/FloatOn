using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector2 moveDirection = Vector2.up;
    Animator anim;
    public int index = 1;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetInteger("index", index);
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void SetMoveDirection(int y)
    {
        moveDirection.y = y;
    }
}
