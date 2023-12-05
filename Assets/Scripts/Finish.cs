using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private int sceneIndex, totalSceneCount;
    public Timer timer;
    [SerializeField] GameObject WinScreen;
    private bool winSequence = false;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        totalSceneCount = SceneManager.sceneCountInBuildSettings;
        WinScreen.SetActive(false);
        //Debug.Log(totalSceneCount);
    }

    private void Update()
    {
        Debug.Log(totalSceneCount);
        //Debug.Log(sceneIndex);
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
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && sceneIndex < totalSceneCount-1)
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
        else if (collision.tag == "Player")
        {
            winSequence = true;
        }

    }
}
