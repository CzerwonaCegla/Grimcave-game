using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAdd = 10f;
    public float remainingTime = 10f;
    [SerializeField] GameObject LoseScreen;
    private float timeFade = 1;

    private void Start()
    {
        EventManager.current.onTimePickupTriggered += TimePickupTriggered;
        LoseScreen.SetActive(false);
    }
    void Update()
    {
        if (remainingTime > 0)
        {
            Debug.Log(remainingTime);
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0.1f)
        {
            if (timeFade > 0.1f)
            {
                Time.timeScale = timeFade;
                timeFade -= Time.deltaTime;
            }
            else
            {
                LoseScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void TimePickupTriggered()
    {
        remainingTime += timeToAdd;
    }
}
