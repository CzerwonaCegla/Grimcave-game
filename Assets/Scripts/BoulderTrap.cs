using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrap : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRend;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float directionX = -0.5f;
    [SerializeField] private float directionY = 0f;
    [SerializeField] private Transform boulderSpawn;
    [SerializeField] private AudioSource breakSoundEffect;
    private Vector3 boulderSpawnPosition;
    //private Vector3[] direction = new Vector3[1];
    private bool attacking = false;
    private bool resetting = false;
    //private bool breaking = false;

    private void OnDrawGizmos()
    {
        // Draw the detection ray in the scene view
        Gizmos.color = Color.red;
        Vector3 start = transform.position + new Vector3(directionX, directionY);
        Vector3 end = start + Vector3.down * range;
        Gizmos.DrawLine(start, end);
    }

    public void ActivateBoulder()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        boulderSpawnPosition = boulderSpawn.position;
        StartCoroutine(CheckForPlayerRoutine());
    }

    private IEnumerator CheckForPlayerRoutine()
    {
        while (true)
        {
            CheckForPlayer();
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void CheckForPlayer()
    {
        if (resetting == false)
        {
            //Vector2 raycastDirection = new Vector2(direction1, direction2);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(directionX, directionY), Vector2.down, range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
            }
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
        attacking = false;
        StartCoroutine(DeactivateAndReset());
    }

    private IEnumerator DeactivateAndReset()
    {
        resetting = true;
        breakSoundEffect.Play();
        circleCollider.enabled = false;
        anim.SetTrigger("Break");
        yield return new WaitForSeconds(0.5f);
        spriteRend.enabled = false;
        transform.position = boulderSpawnPosition;
        yield return new WaitForSeconds(2f);
        circleCollider.enabled = true;
        spriteRend.enabled = true;
        anim.SetTrigger("Idle");
        resetting = false;
    }
}