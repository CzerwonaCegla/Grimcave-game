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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.current.onCheckPointRespawnTriggered += CheckPointRespawnTriggered;
    }

    private void CheckPointRespawnTriggered()
    {
        // Delay the execution by seconds (adjust the time as needed)
        float delay = 0.5f;
        Invoke("RespawnPlayerAtCheckPoint", delay);
    }

    private void RespawnPlayerAtCheckPoint()
    {
        deathPlace = player.transform.position;
        tpSoundEffect.Play();
        player.transform.position = player.GetComponent<CheckPointDetect>().currentCheckPoint;
        Instantiate(playerCorpse, deathPlace, Quaternion.identity);
    }
}
