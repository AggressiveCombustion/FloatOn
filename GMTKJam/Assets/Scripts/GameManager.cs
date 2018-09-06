using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public float speed = 1.0f;
    public float gravity = 1.0f;

    public List<Timer> timers = new List<Timer>();

    public int act = 1;
    public int scene = 1;

    public GameObject popped;
    public GameObject explosion;

    bool canLoadLevel = true;

	// Use this for initialization
	void Start () {

        if (instance == null)
            instance = this;

        else if (instance != this)
        {
            Destroy(gameObject); // this?
            Destroy(this); // this?
        }

        // persistent!
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
        for(int i = 0; i < timers.Count; i++)
        {
            timers[i].Update();

            if(timers[i].done)
            {
                timers.RemoveAt(i);
            }
        }
	}

    // function overloads, yay!
    public void AddTimer(Timer t)
    {
        // add to list of timers
        timers.Add(t);
    }

    public void AddTimer(float duration, TimerEvent myEvent)
    {
        Timer t = new Timer();
        t.duration = duration;
        t.myEvent = myEvent;

        AddTimer(t);
    }

    public void FinishLevel()
    {
        //Debug.Log("FinishLevel");
        // Do end of level stuff
        // Maybe an animation?

        // Go to the next level
        AddTimer(1.5f, FadeToBlack);
        AddTimer(3.5f, GoToNextLevel);
    }

    public void GoToNextLevel()
    {
        if (!canLoadLevel)
            return;

        canLoadLevel = false;
        scene += 1;
        if (scene > 3)
        {
            act += 1;
            scene = 1;
        }

        if(act == 4 && scene > 1)
        {
            // you beat the game!
            SceneManager.LoadScene("Final");
            //act = 1;
            //scene = 1;
            return;
        }

        SceneManager.LoadScene(act + "-" + scene);

    }

    public void FadeToBlack()
    {
        GameObject.Find("Fade").GetComponent<Fade>().FadeToBlack();
        canLoadLevel = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(act + "-" + scene);
    }

    public void GoToMainMenu()
    {
        instance.act = 1;
        instance.scene = 1;
        SceneManager.LoadScene("Start");
    }

    public void GoToLevelSelect()
    {
        instance.act = 1;
        instance.scene = 1;
        SceneManager.LoadScene("LevelSelect");

        
    }

    public void GoToOptions()
    {
        instance.act = 1;
        instance.scene = 1;
        SceneManager.LoadScene("Options");
    }

    public void ExitToDesktop()
    {
        AddTimer(2.0f, Application.Quit);
    }

    public void StartAct()
    {
        SceneManager.LoadScene(act + "-" + scene);
        /*PlayerInMenu[] pIM = FindObjectsOfType<PlayerInMenu>();
        for(int i = 0; i < pIM.Length; i++)
        {
            if (pIM[i] != null)
            {
                Destroy(pIM[i]);
                pIM[i] = null;
            }
        }

        Destroy(GameObject.Find("Player In Menu"));*/
        GameObject pim = GameObject.Find("Player In Menu");
        if(pim != null)
            pim.GetComponent<PlayerInMenu>().Explode();
    }

    public void SetGasAmount(float amount)
    {
        Image gasBar = GameObject.Find("GasBar").GetComponent<Image>();
        Image gasRing = GameObject.Find("GasRing").GetComponent<Image>();

        if (gasBar != null && gasRing != null)
        {

            gasBar.fillAmount = amount;
            //gasRing.fillAmount = amount - 1.0f;
            gasRing.fillAmount = ((amount - 1) / 1.5f) * 1.0f;

            GameObject.Find("GasParent").GetComponent<Shake>().enabled = amount >= 2;
        }
    }

    public void AddShake(float duration, string nameOfObject, float intensity)
    {
        GameObject g = GameObject.Find(nameOfObject);
        if(g != null)
        {
            if (g.GetComponent<Shake>() != null)
                return;
            g.AddComponent<Shake>();
            g.GetComponent<Shake>().duration = duration;
            g.GetComponent<Shake>().intensity = intensity;
        }
    }
}

public delegate void TimerEvent();

public class Timer
{
    public float elapsed = 0;
    public float duration;
    public TimerEvent myEvent;
    public bool done = false;

    public void Update()
    {
        elapsed += Time.deltaTime;

        if(elapsed >= duration)
        {
            myEvent();
            done = true;
        }
    }
}
