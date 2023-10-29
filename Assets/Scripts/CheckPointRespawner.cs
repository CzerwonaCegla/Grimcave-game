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
        player.transform.position = player.GetComponent<CheckPointDetect>().currentCheckPoint;
    }
}
