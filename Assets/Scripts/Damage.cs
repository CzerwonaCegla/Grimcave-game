using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] protected float damage;
    

    protected Timer GetTimer()
    {
        if (Timer.instance)
        {
            return Timer.instance;
        }

        return null;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GetTimer() != null && GetTimer().remainingTime > 0f)
            {
                GetTimer().remainingTime -= damage;
            }
        }
            
    }
}
