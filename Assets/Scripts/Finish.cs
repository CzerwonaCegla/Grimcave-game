using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private Scene scene;
    public Timer timer;
    [SerializeField] GameObject WinScreen;
    private bool winSequence = false;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
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
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && SceneManager.GetSceneByBuildIndex(scene.buildIndex+1) == null)
        {
            winSequence = true;
        }
        else
        {
            SceneManager.LoadScene(scene.buildIndex+1);
        }

    }
}
