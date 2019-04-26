using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    private int Score = 0;

    private float pauseEnd = 0;
    private float Timer;
    private float TimeAttackTotalTime;

    private InGameUi GameUI;


    public enum Mode { Normal, TimeAttack }
    public Mode activeMode;


    public delegate void modeUpdate();
    modeUpdate curMode = null;




    // on object creation
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("inGameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        GameUI = gameObject.transform.GetComponentInChildren<InGameUi>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame(Mode.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (curMode != null)
        {
            curMode();
        }

    }


    /// <summary>
    /// Starts the game in the mode that is passed in, also takes an optional time value if you are in time attak mode
    /// </summary>
    /// <param name="gameMode">The game mode to be started normal or time attack</param>
    /// <param name="time">The amount of time for time trials</param>
    public void StartGame(Mode gameMode, float time = 0)
    {
        TimeAttackTotalTime = time;
        Timer = 0f;
        // this will call the approprite functions for each mode i've chosen to seperate the mode start logic in to private functions.
        setMode(gameMode, time);


        // currently all Start games use the same scene this can easily be refactore 
        // if need be in the future by moving this call in to the switch
        SceneManager.LoadScene("TileCreation");

    }


    /// <summary>
    /// Sets the mode based on input
    /// </summary>
    /// <param name="updateMode">Mode To Update To</param>
    /// <param name="time">time to set if we are setting time attack</param>
    private void setMode(Mode updateMode, float time)
    {
        switch (updateMode)
        {
            case Mode.Normal:
                StartNormal();
                break;

            case Mode.TimeAttack:
                StartTimeAttack(time);
                break;
        }
    }


    private void StartTimeAttack(float time)
    {
        Timer = time;
        curMode = new modeUpdate(TimeAttackUpdate);
        activeMode = Mode.TimeAttack;
    }

    private void StartNormal()
    {
        curMode = new modeUpdate(NormalUpdate);
        activeMode = Mode.Normal;
    }

    private void NormalUpdate()
    {
        Timer = Timer + Time.deltaTime;
        GameUI.SetTimer(Timer);
    }

    private void TimeAttackUpdate()
    {
        Timer = Timer - Time.deltaTime;
        GameUI.SetTimer(Timer);

        if (Timer <= 0f)
        {
            GameUI.SetTimer(Timer);
            Puase();
            GameUI.setCenteredText(string.Format("Congratulations you finished {0} levels in {1} ",Score, TimeAttackTotalTime / 60.0f));
        }

    }




    public void HitGoal()
    {
        switch (activeMode)
        {
            case Mode.Normal:
                GameUI.setCenteredText(string.Format("Congratulations you finished this maze in {0}", GameUI.secondsToPrintableTime(Timer)));
                Puase();
                break;
            case Mode.TimeAttack:
                Score++;
                GameUI.SetScore(Score);
                Puase(3.0f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }


    public void Puase(float puaseLangth = -666)
    {
        // turns off all physics time
        Time.timeScale = 0f;
        GameUI.SetPanal(true);

        if (puaseLangth > 0)
        {
            pauseEnd = Time.realtimeSinceStartup + puaseLangth;
            GameUI.setCountDown(puaseLangth, true);
            curMode = new modeUpdate(CountDownPuase);
        }
        else
        {
            curMode = new modeUpdate(PuaseUpdate);
        }

    }

    private void PuaseUpdate()
    {
        // TODO check if this is a count down
    }

    private void CountDownPuase()
    {

        if (pauseEnd > Time.realtimeSinceStartup)
        {
            GameUI.setCountDown(pauseEnd - Time.realtimeSinceStartup, true);
        }
        else
        {
            GameUI.SetPanal(false);
            GameUI.setCountDown(0, false);
            Time.timeScale = 1f;
            setMode(activeMode, Timer);
        }
    }
}
