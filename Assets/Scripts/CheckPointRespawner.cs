using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheckPointRespawner : MonoBehaviour
{
    private GameObject player;
    private Vector2 deathPlace;
    [SerializeField] GameObject playerCorpse;
    [SerializeField] private AudioSource tpSoundEffect;
    private bool isDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.current.onCheckPointRespawnTriggered += CheckPointRespawnTriggered;
    }

    private void CheckPointRespawnTriggered()
    {
        if (!isDead)
        {
            StartCoroutine(Respawn());
        }
    }

    private void RespawnPlayerAtCheckPoint()
    {
        tpSoundEffect.Play();
        player.transform.position = player.GetComponent<CheckPointDetect>().currentCheckPoint;
    }
    
    private IEnumerator Respawn()
    {
        isDead = true;
        deathPlace = player.transform.position;
        Instantiate(playerCorpse, deathPlace, Quaternion.identity);
        // Delay the execution by seconds (adjust the time as needed)
        yield return new WaitForSeconds(0.5f);
        //float delay = 0.5f;
        // Invoke("RespawnPlayerAtCheckPoint", delay);
        RespawnPlayerAtCheckPoint();
        isDead = false;
    }
}
