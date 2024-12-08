using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimedAnimation : MonoBehaviour
{
    public TMP_Text countdown;
    public float duration;
    public Animator myAnimator;
    public string clipName;

    float timer;
    bool countingDown;

    void FixedUpdate()
    {
        if (countingDown) 
        {
            timer -= Time.fixedDeltaTime; //Count down

            countdown.text = "" + Mathf.RoundToInt(timer) + "s"; //Update timer text

            if (timer <= 0)
            {
                countingDown = false;
                myAnimator.Play(clipName);
            }
        }
    }

    public void StartTimer() 
    {
        countingDown = true;
        timer = duration;
    }
}
