using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            NormalCanvas.SetActive(true);
            MenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            isMenuOpen = false;
        }
        else if (!isMenuOpen)
        {
            NormalCanvas.SetActive(false);
            MenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            isMenuOpen = true;
        }
    }
}
