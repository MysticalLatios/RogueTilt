using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    private float Time_Left;
    private float FreezeStartPivit;
    private float countDownTop;
    public Text countdownText;
    public Text levelTimer;
    public Text score;
    public Text CenteredText;
    public Image pannal;
    public bool LevelStartFreeze = false;
    public bool LevelTimerActive = false;
    private float Timer = 60*1;
    private float TIME = 60*1;
    private int ScoreTracker = 0;


    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void HitGoal()
    {
        ScoreTracker++;
        score.text = string.Format("Score : {0}", ScoreTracker);
        PuaseTimer();
        RestartLevel();
    }

    /// <summary>
    /// Sets the in game UI timer to the amount of time passed in as secounds 
    /// </summary>
    /// <param name="seconds">Seconds to be displayed in form min:secound:milisec</param>
    public void SetTimer(float seconds)
    { 
        levelTimer.text = string.Format(secondsToPrintableTime(seconds));
    }


    /// <summary>
    /// Returns a string in the format 00:00:00 min:secound:milisec
    /// </summary>
    /// <returns>String in the format 00:00:00 min:secound:milisec</returns>
    /// <param name="seconds">Secounds.</param>
    public string secondsToPrintableTime(float seconds)
    {
        int min = Mathf.FloorToInt(seconds / 60);
        float sec = seconds - (60 * min);
        float microsec = Mathf.Floor((seconds - Mathf.Floor(seconds))*100);
        return string.Format("{0:00}:{1:00}:{2:00}", min, sec, microsec);
    }


    /// <summary>
    /// Sets the UI score board to show the int as your current score
    /// </summary>
    /// <param name="scoreIn">score to be displayed</param>
    public void SetScore(int scoreIn)
    {
        score.text = string.Format("Score : {0}", scoreIn);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="set"></param>
    public void SetPanal(bool set)
    {
        pannal.gameObject.SetActive(set);
    }

    /// <summary>
    /// /
    /// </summary>
    /// <param name="num">Number.</param>
    /// <param name="set">If set to <c>true</c> set.</param>
    public void setCountDown(float num = 0, bool set = true)
    {
        countdownText.gameObject.SetActive(set);
        countdownText.text = string.Format("{0:0.00}", num);
    }

    /// <summary>
    /// Sets the timer active.
    /// </summary>
    /// <param name="set">If set to <c>true</c> set.</param>
    public void setTimerActive(bool set)
    {
        levelTimer.gameObject.SetActive(set);
    }

    /// <summary>
    /// Sets the centered text.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="set">If set to <c>true</c> set.</param>
    public void setCenteredText(string text, bool set = true)
    {
        CenteredText.text = text;
        CenteredText.gameObject.SetActive(set);

    }


}
