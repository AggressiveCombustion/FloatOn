using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    Vector3 startPos;
    public bool constant = false;
    public float duration = 1.0f;
    public float intensity = 1.0f;
    float elapsed;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        //transform.position = startPos;

        if (!constant)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= duration)
            {
                transform.position = startPos;
                Destroy(this);
            }
        }

        //startPos = transform.position;
        Vector2 shake = (new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) / 10) * intensity;
        transform.position = new Vector3(startPos.x + shake.x, startPos.y + shake.y, transform.position.z);
    }
}
