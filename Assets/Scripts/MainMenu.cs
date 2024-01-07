using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource buttonPressSound;
    public void PlayGame()
    {
        buttonPressSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        buttonPressSound.Play();
        Application.Quit();
        Debug.Log("Donna mama es mi hujocita");
    }

}
