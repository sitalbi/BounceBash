using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 10;
    [NonSerialized] public bool timerIsRunning = false;
    private Slider slider;
    
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            slider.value = timeRemaining;
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.unscaledDeltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    
    
}
