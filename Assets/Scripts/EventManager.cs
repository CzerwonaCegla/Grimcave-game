using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    public event Action onTimePickupTriggered;
    public void TimePickupTriggered()
    {
        if (onTimePickupTriggered != null)
        {
            onTimePickupTriggered();
        }
    }
}
