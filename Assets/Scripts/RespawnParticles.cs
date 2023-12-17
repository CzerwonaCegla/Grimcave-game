using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnParticles : MonoBehaviour
{
    [DoNotSerialize]public GameObject Go;
    //[SerializeField] public ParticleSystem respawnParticle;

    // Start is called before the first frame update
    void Start()
    {
        //var em = respawnParticle.emission;
        //em.rateOverTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            Go = collision.gameObject;
        }
        if (collision.CompareTag("Traps"))
        {
            StartCoroutine(Go.GetComponent<CheckPoint>().particleKor());
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
