using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float directionX = -0.5f;
    [SerializeField] private float directionY = 0f;
    [SerializeField] private Transform boulderSpawn;
    private Vector3 boulderSpawnPosition;
    //private Vector3[] direction = new Vector3[1];
    private bool attacking = false;

    private void OnDrawGizmos()
    {
        // Draw the detection ray in the scene view
        Gizmos.color = Color.red;
        Vector3 start = transform.position + new Vector3(directionX, directionY);
        Vector3 end = start + Vector3.down * range;
        Gizmos.DrawLine(start, end);
    }

    private void Start()
    {
        StartCoroutine(CheckForPlayerRoutine());
    }

    private IEnumerator CheckForPlayerRoutine()
    {
        while (true)
        {
            CheckForPlayer();
            yield return new WaitForSeconds(checkDelay);
        }
    }

    private void CheckForPlayer()
    {
        //Vector2 raycastDirection = new Vector2(direction1, direction2);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(directionX, directionY), Vector2.down, range, playerLayer);

        if (hit.collider != null && !attacking)
        {
            attacking = true;
        }
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(DeactivateAndReset());
    }

    private IEnumerator DeactivateAndReset()
    {
        Debug.Log("Deactivating and resetting...");
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        transform.position = boulderSpawnPosition;
        Debug.Log("Reset position. Reactivating...");
        gameObject.SetActive(true);
        attacking = false;
    }
}