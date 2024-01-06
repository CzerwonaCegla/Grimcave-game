using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    
    [SerializeField] private float timeToAdd = 10f;
    public float remainingTime = 10f;

    [SerializeField] GameObject LoseScreen;
    [SerializeField] GameObject RestartScreen;
    [SerializeField] TextMeshProUGUI tmpTimerText;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource buttonPressSound;
    [SerializeField] private AudioSource CountdownSound;

    [DoNotSerialize] public float timeFade = 1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        EventManager.current.onTimePickupTriggered += TimePickupTriggered;
        LoseScreen.SetActive(false);
        RestartScreen.SetActive(false);
    }

    bool lowTimerOneShot = true;
    void Update()
    {
        if (remainingTime > 0)
        {
            //Debug.Log(remainingTime);
            remainingTime -= Time.deltaTime;
            float rounded = (float)Math.Round(remainingTime, 2);
            tmpTimerText.text = rounded.ToString();

            if (remainingTime <= 15f && lowTimerOneShot)
            {
                tmpTimerText.color = Color.red;
                tmpTimerText.fontSize = 65;
                CountdownSound.Play();

                lowTimerOneShot = false;
            }
            else if (remainingTime > 15f)
            {
                tmpTimerText.color = Color.white;
                tmpTimerText.fontSize = 40;
                CountdownSound.Stop();
                lowTimerOneShot = true;
            }
        }  
        else if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            tmpTimerText.text = remainingTime.ToString();

            if (timeFade > 0.1f)
            {
                deathSoundEffect.Play();
                Time.timeScale = timeFade;
                timeFade -= Time.deltaTime;
            }
            else
            {
                LoseScreen.SetActive(true);
                RestartScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void TimePickupTriggered()
    {
        remainingTime += timeToAdd;
    }

    public void Restart()
    {
        buttonPressSound.Play();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
