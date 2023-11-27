using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    //Color colour; //placeholder do animacji
    BoxCollider2D colider;
    private bool CRrunning;
    //private int animationStage;
    void Start()
    {
        colider = GetComponent<BoxCollider2D>();
        //colour = GetComponent<Renderer>().material.color; //placeholder 2
        //animationStage = 0;
        colider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CRrunning)
        {
            StartCoroutine(PlatformDestruction());
        }
    }

    private IEnumerator PlatformDestruction()
    {
        CRrunning = true;

        // stage 2
        //animationStage++;
        GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(2);

        // stage 3
        //animationStage++;
        GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(2);

        // stage 4 - platform break
        //animationStage++;
        GetComponent<Renderer>().material.color = Color.cyan;
        colider.enabled = false;
        yield return new WaitForSeconds(4);

        // stage 5 - platform respawn
        GetComponent<Renderer>().material.color = Color.white;
        //animationStage = 0;
        colider.enabled = true;

        CRrunning = false;
    }
    //enum ze stagami
    public enum PlatformStages 
    { 
        Idle;
        Breaking1;
        Breaking2;
        Breaking3;
        Breaking4;
        Breaking5;
        Gone;
    }
}
