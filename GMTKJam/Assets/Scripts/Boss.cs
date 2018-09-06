using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public Sprite[] sprites = new Sprite[2];
    public GameObject cannonball;

    public float elapsed = 0.0f;

    bool step1 = false;
    bool step2 = false;
    bool step3 = false;

    public GameObject cage;

    public float hp = 5;
    Color myColor;

    public AudioClip spitSound;

    // Use this for initialization
    void Start()
    {
        myColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

        elapsed += Time.deltaTime;

        if (elapsed >= 4.5)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            elapsed = 0;
            step3 = false;
            step2 = false;
            step1 = false;
        }

        else if (elapsed >= 4.0f && !step3)
        {
            FireBall();
            step3 = true;
        }
        else if (elapsed >= 3.5f && !step2)
        {
            //GetComponent<SpriteRenderer>().sprite = sprites[0];
            FireBall();
            step2 = true;
        }

        else if (elapsed >= 3.0f && !step1)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            step1 = true;
        }

        GameObject[] cannonballs = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < cannonballs.Length; i++)
        {
            if (cannonballs[i].GetComponent<Cannonball>().ready)
            {
                float distance = Vector2.Distance(GameObject.Find("BossEye").transform.position, cannonballs[i].transform.position);
                if (distance < 1f)
                {
                    // destroy
                    cannonballs[i].GetComponent<Cannonball>().Explode();
                    //Dead();
                    TakeDamage();
                }
            }
        }

        SetBarFill();

    }

    void SetBarFill()
    {
        GameObject.Find("EnemyBar").GetComponent<Image>().fillAmount = hp / 5.0f;
    }

    void TakeDamage()
    {
        hp -= 1;
        if(hp <= 0)
        {
            Dead();
        }

        GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.instance.AddTimer(0.5f, ResetShader);
    }

    void ResetShader()
    {
        if (GameObject.Find("boss1") == null)
            return;
        GetComponent<SpriteRenderer>().color = myColor;
    }

    void FireBall()
    {
        GameObject c = Instantiate(cannonball, GameObject.Find("BossMouth").transform.position, Quaternion.Euler(0, 0, 0));
        c.GetComponent<Cannonball>().owner = gameObject;
        c.GetComponent<SpriteRenderer>().color = myColor;

        GetComponent<AudioSource>().clip = spitSound;
        GetComponent<AudioSource>().Play();
    }

    void Dead()
    {
        if (cage != null)
        {
            Destroy(cage);
        }
        Destroy(gameObject);
    }
}
