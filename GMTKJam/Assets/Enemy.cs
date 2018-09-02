using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Sprite[] sprites = new Sprite[2];
    public Transform spitter;
    public GameObject cannonball;

    public float elapsed = 0.0f;

    bool step1 = false;
    bool step2 = false;
    bool step3 = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        AimSpitter();

        elapsed += Time.deltaTime;

        if (elapsed >= 4.5)
        {
            spitter.GetComponentInChildren<SpriteRenderer>().enabled = false;
            elapsed = 0;
            step3 = false;
            step2 = false;
            step1 = false;
        }

        else if (elapsed >= 4.0f && !step2)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            spitter.GetComponentInChildren<SpriteRenderer>().enabled = true;
            FireBall();
            step2 = true;
        }

        else if (elapsed >= 3.0f && !step1)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            step1 = true;
        }

        


        
	}

    void AimSpitter()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
            return;

        float distance = Vector2.Distance(player.position, spitter.transform.position);

        float angleRad = Mathf.Atan2(player.position.y - spitter.transform.position.y,
                                     player.position.x - spitter.transform.position.x);
        float angle = (180 / Mathf.PI) * angleRad;

        spitter.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        
    }

    void FireBall()
    {
        GameObject c = Instantiate(cannonball, transform.position, Quaternion.Euler(0, 0, 0));
        c.GetComponent<Cannonball>().owner = gameObject;
    }
}
