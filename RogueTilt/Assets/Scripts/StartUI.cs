using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private float Time_Left;
    private float FreezeStartPivit;
    private float countDownTop;
    public Text countdownText;
    public Image pannal;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }


    public void StartFreeze(float countdown) 
    {
        this.gameObject.SetActive(true);
        pannal.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);
        countdownText.text = "";
        Time.timeScale = 0f;
        FreezeStartPivit = Time.realtimeSinceStartup;
        countDownTop = countdown;
    }


}
