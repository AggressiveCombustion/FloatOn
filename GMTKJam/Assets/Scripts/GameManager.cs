using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public float speed = 1.0f;
    public float gravity = 1.0f;

    public List<Timer> timers = new List<Timer>();

    public int act = 1;
    public int scene = 1;

    public GameObject popped;
    public GameObject explosion;

	// Use this for initialization
	void Start () {

        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(this);

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
        scene += 1;
        if (scene > 4)
            act += 1;

        if(act > 4)
        {
            // you beat the game!
        }

        SceneManager.LoadScene(act + "-" + scene);
        /*string levelName = SceneManager.GetActiveScene().name;
        if (levelName.Length == 3)
        {
            int act = name[0];
            int scene = name[2];

            Debug.Log("Current level is <" + act + "-" + scene + ">");

            int next = name[2] + 1;
            if (next > 4)
            {
                next = 1;
                act += 1;
            }

            string nextLevel = act + "-" + next; // 1-4, for instance
            Debug.Log("Preparing to Load <" + nextLevel + ">");
            SceneManager.LoadScene(nextLevel);*/

    }

    public void FadeToBlack()
    {
        GameObject.Find("Fade").GetComponent<Fade>().FadeToBlack();
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
