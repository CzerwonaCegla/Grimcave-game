using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] float activeTime;
    [SerializeField] float attackCooldown;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private float cooldownTimer;
    private bool active;
    [Space(10)]
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    [SerializeField] Sprite sprite3;
    [SerializeField] Sprite sprite4;
    [SerializeField] Sprite sprite5;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            StartCoroutine(Attack());
            cooldownTimer = Time.deltaTime;
        }
            
    }

    private IEnumerator Attack()
    {
        gameObject.SetActive(true);
        active = true;
        spriteRend.sprite = sprite1;
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite2;
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite3;
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite4;
        yield return new WaitForSeconds(activeTime);
        spriteRend.sprite = sprite3;
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite2;
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite1;
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite5;
        active = false;
        cooldownTimer = 0;    
    }
}
