using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Timer timer;
    [SerializeField] GameObject WinScreen;
    private bool winSequence = false;

    private void Start()
    {
        WinScreen.SetActive(false);
    }

    private void Update()
    {
        if (winSequence == true)
        {
            if (timer.timeFade > 0.1f)
            {
                Time.timeScale = timer.timeFade;
                timer.timeFade -= Time.deltaTime;
            }
            else
            {
                WinScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            winSequence = true;
        }

    }
}
