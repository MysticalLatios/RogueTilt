using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private float Time_Left;
    private float FreezeStartPivit;
    private float countDownTop;
    public Text countdownText;
    public Text levelTimer;
    public Text score;
    public Image pannal;
    public bool LevelStartFreeze = false;
    public bool LevelTimerActive = false;
    private float Timer = 60*1;
    private float TIME = 60*1;
    private int ScoreTracker = 0;


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("startUI");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Timer = Timer - Time.deltaTime;
        int min = Mathf.FloorToInt(Timer / 60);
        float sec = Timer - (60 * min);
        float microsec = Mathf.Floor((Timer - Mathf.Floor(Timer)));

        levelTimer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, microsec);
        score.text = "Score : 0";
    }

    // Update is called once per frame
    void Update()
    {
        // Handel when the level starts and we want a countdown to show puase
        if (LevelStartFreeze)
        {
            Time_Left = (Time.realtimeSinceStartup - FreezeStartPivit);
            if (Time_Left < countDownTop)
            {
                countdownText.text = (Mathf.Ceil(countDownTop - Time_Left)).ToString();
            }
            else
            {
                pannal.gameObject.SetActive(false);
                countdownText.gameObject.SetActive(false);
                Time.timeScale = 1;
                LevelStartFreeze = false;

                LevelTimerActive = true;
            }
        }
        // this part will run if our timer should be un it will count down and end the game if it runs out.
        else if (LevelTimerActive)
        {
            if (Timer > 0.0f)
            {
                Timer = Timer - Time.deltaTime;
                int min = Mathf.FloorToInt(Timer / 60);
                float sec = Mathf.Floor(Timer - (60*min));
                float microsec = Mathf.Floor((Timer - Mathf.Floor(Timer))*100);

                levelTimer.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, microsec);
            }
            else
            {
                levelTimer.text = string.Format("{0:00}:{1:00}:{2:00}", 0, 0, 0);
                LevelTimerActive = false;
                pannal.gameObject.SetActive(true);
                countdownText.gameObject.SetActive(true);
                countdownText.text = string.Format("congrats you compleated {0} rooms in {1:0.00} minuites",ScoreTracker,(TIME / 60));
                Time.timeScale = 0f;

            }

        }

    }


    public void StartFreeze(float countdown) 
    {
        LevelStartFreeze = true;
        pannal.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);
        countdownText.text = "";
        Time.timeScale = 0f;
        FreezeStartPivit = Time.realtimeSinceStartup;
        countDownTop = countdown;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PuaseTimer()
    {
        LevelTimerActive = false;
    }

    public void hitGoal()
    {
        ScoreTracker++;
        score.text = string.Format("Score : {0}", ScoreTracker);
        PuaseTimer();
        RestartLevel();
    }


}
