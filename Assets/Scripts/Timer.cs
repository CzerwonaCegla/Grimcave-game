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

    [DoNotSerialize] public float timeFade = 1;

    private void Awake()
    {
        if(instance != null)
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
    void Update()
    {
        if (remainingTime > 0)
        {
            //Debug.Log(remainingTime);
            remainingTime -= Time.deltaTime;
            float rounded = (float)Math.Round(remainingTime, 2);
            tmpTimerText.text = rounded.ToString();
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
