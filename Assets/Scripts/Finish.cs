using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Finish : MonoBehaviour
{
    private int sceneIndex, totalSceneCount;
    public Timer timer;
    [SerializeField] GameObject WinScreen;
    private bool winSequence = false;
    [SerializeField] private AudioSource finishSoundEffect;
    [SerializeField] private float fadeSpeed = 1.0f;
    [SerializeField] private TextMeshProUGUI tmpLevelText;
    [SerializeField] GameObject playerRef;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        totalSceneCount = SceneManager.sceneCountInBuildSettings;
        WinScreen.SetActive(false);
        tmpLevelText.text = sceneIndex.ToString();
        //Debug.Log(totalSceneCount);
    }

    private void Update()
    {
        if (!winSequence) 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { SceneManager.LoadScene(1); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { SceneManager.LoadScene(2); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { SceneManager.LoadScene(3); }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { SceneManager.LoadScene(4); }
            if (Input.GetKeyDown(KeyCode.Alpha5)) { SceneManager.LoadScene(5); }
            if (Input.GetKeyDown(KeyCode.Alpha6)) { SceneManager.LoadScene(6); }
        }
        //Debug.Log(totalSceneCount);
        //Debug.Log(sceneIndex);
        if (winSequence == true)
        {
            playerRef.GetComponent<NewMovement>().enabled = false;
            if (timer.timeFade > 0.1f)
            {
                playerRef.GetComponent<AudioSource>().enabled = false;
                WinScreen.SetActive(true);
                Time.timeScale = timer.timeFade;
                timer.timeFade -= Time.deltaTime * fadeSpeed;
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
            finishSoundEffect.Play();
            NewMovement movementScript = collision.GetComponent<NewMovement>();
            if (movementScript != null)
            {
                movementScript.enabled = false;
            }
        }

    }
}
