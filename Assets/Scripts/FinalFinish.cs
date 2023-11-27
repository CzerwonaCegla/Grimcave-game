using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalFinish : MonoBehaviour
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
                WinScreen.SetActive(true);
                Time.timeScale = timer.timeFade;
                timer.timeFade -= Time.deltaTime;
            }
            else
            {
                WinScreen.SetActive(false);
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1;
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
