using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] float activeTime;
    [SerializeField] float attackCooldown;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private float cooldownTimer;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        //cooldownTimer = 0;
        active = true;
        yield return new WaitForSeconds(activeTime);
        active = false;
    }
}
