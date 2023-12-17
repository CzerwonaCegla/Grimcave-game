using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointDetect : MonoBehaviour
{
    [DoNotSerialize] public Vector2 currentCheckPoint;
    private void Start()
    {
        //var em = respawnParticle.emission;
        //em.rateOverTime = 0;
        //respawnParticle.Stop();
        //respawnParticle.Clear();
        currentCheckPoint = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            //currentCheckPoint = collision.gameObject.transform.position;
            currentCheckPoint = collision.gameObject.transform.position;
            //collision.gameObject.SetActive(false);
            CheckPoint checkPointScript = collision.gameObject.GetComponent<CheckPoint>();
            if (checkPointScript != null)
            {
                // Call the TriggerChange method on the CheckPoint script
                checkPointScript.TriggerChange();
            }
        }
        if (collision.CompareTag("Traps"))
        {
            EventManager.current.CheckPointRespawnTriggered();
            //StartCoroutine(particleKor());
        }
    }
    //public IEnumerator particleKor()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    var em = respawnParticle.emission;
    //    em.rateOverTime = 80;
    //    yield return new WaitForSeconds(1f);
    //    em.rateOverTime = 0;
    //}
}
