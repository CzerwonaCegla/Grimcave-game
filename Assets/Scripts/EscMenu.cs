using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] GameObject NormalCanvas;
    [SerializeField] GameObject MenuCanvas;
    [SerializeField] private bool isMenuOpen;

    private void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DoMenuStuff();
        }
    }
    public void Resume()
    {
        DoMenuStuff();
    }

    private void DoMenuStuff()
    {
        if (isMenuOpen)
        {
            AudioListener.volume = 1f;
            NormalCanvas.SetActive(true);
            MenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            isMenuOpen = false;
        }
        else if (!isMenuOpen)
        {
            AudioListener.volume = 0f;
            NormalCanvas.SetActive(false);
            MenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            isMenuOpen = true;
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
