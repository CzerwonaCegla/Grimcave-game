using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerTag;
    private Vector3 destination;
    private Vector3[] direction = new Vector3[1];

    private bool attacking;

    private void Update()
    {
        if (attacking)
        transform.Translate(destination * Time.deltaTime * speed);
        else
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        CalculateDirection();
            for (int i = 0; i < direction.Length; i++)
        {
            Debug.DrawRay(transform.position, direction[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction[i], range, playerTag);
        }
    }

    private void CalculateDirection()
    {
        direction[0] = -transform.up * range;
    }
}
