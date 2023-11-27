using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheckPointRespawner : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.current.onCheckPointRespawnTriggered += CheckPointRespawnTriggered;
    }

    private void CheckPointRespawnTriggered()
    {
        // Delay the execution by 2 seconds (adjust the time as needed)
        float delay = 0.5f;
        Invoke("RespawnPlayerAtCheckPoint", delay);
    }

    private void RespawnPlayerAtCheckPoint()
    {
        player.transform.position = player.GetComponent<CheckPointDetect>().currentCheckPoint;
    }
}
