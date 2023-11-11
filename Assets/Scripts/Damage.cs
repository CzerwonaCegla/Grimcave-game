using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] protected float damage;
    public Timer timer;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (timer != null && timer.remainingTime > 0f)
            {
                timer.remainingTime -= damage;
            }
        }
            
    }
}
