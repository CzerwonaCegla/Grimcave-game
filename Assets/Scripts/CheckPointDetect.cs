using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointDetect : MonoBehaviour
{
    [DoNotSerialize] public Vector2 currentCheckPoint;

    private void Start()
    {
        currentCheckPoint = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            currentCheckPoint = collision.gameObject.transform.position;
            //collision.gameObject.SetActive(false);
            CheckPoint checkPointScript = collision.GetComponent<CheckPoint>();
            if (checkPointScript != null)
            {
                // Call the TriggerChange method on the CheckPoint script
                checkPointScript.TriggerChange();
            }
        }
        if (collision.CompareTag("Traps"))
        {
            EventManager.current.CheckPointRespawnTriggered();
        }
    }
}
