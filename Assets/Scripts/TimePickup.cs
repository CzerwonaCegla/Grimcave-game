using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.current.TimePickupTriggered();
            GameObject.Destroy(gameObject);
        }
    }
}
