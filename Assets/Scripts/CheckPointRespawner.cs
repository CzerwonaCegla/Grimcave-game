using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheckPointRespawner : MonoBehaviour
{
    private GameObject player;
    private Vector2 deathPlace;
    [SerializeField] GameObject playerCorpse;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.current.onCheckPointRespawnTriggered += CheckPointRespawnTriggered;
    }

    private void CheckPointRespawnTriggered()
    {
        deathPlace = player.transform.position;
        player.transform.position = player.GetComponent<CheckPointDetect>().currentCheckPoint;
        Instantiate(playerCorpse, deathPlace, Quaternion.identity);
    }
}
