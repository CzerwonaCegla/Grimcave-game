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
    //private bool active;
    [Space(10)]
    [SerializeField] private AudioSource igniteSoundEffect;
    [SerializeField] private AudioSource burnSoundEffect;
    [SerializeField] private AudioSource exitSoundEffect;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    [SerializeField] Sprite sprite3;
    [SerializeField] Sprite sprite4;
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
        //cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            StartCoroutine(Attack());
            cooldownTimer = Time.deltaTime;
        }
        else
        {
            cooldownTimer += Time.deltaTime;
        }
            
    }

    private IEnumerator Attack()
    {
        igniteSoundEffect.Play();
        anim.enabled = false;
        spriteRend.enabled = true;
        boxCollider.enabled = true;
        burnSoundEffect.Play();
        spriteRend.sprite = sprite1;
        boxCollider.offset = new Vector2(0f, 0.6f);
        boxCollider.size = new Vector2(0.6f, 0.9f);
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite2;
        boxCollider.offset = new Vector2(0f, 1.5f);
        boxCollider.size = new Vector2(0.6f, 2.7f);
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite3;
        boxCollider.offset = new Vector2(0f, 2.22f);
        boxCollider.size = new Vector2(0.6f, 4f);
        yield return new WaitForSeconds(0.1f);
        spriteRend.sprite = sprite4;
        anim.enabled = true;
        boxCollider.size = new Vector2(0.6f, 6f);
        boxCollider.offset = new Vector2(0f, 3f);
        yield return new WaitForSeconds(activeTime);
        burnSoundEffect.Stop();
        exitSoundEffect.Play();
        anim.enabled = false;
        spriteRend.sprite = sprite2;
        boxCollider.offset = new Vector2(0f, 1.5f);
        boxCollider.size = new Vector2(0.6f, 2.7f);
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;
        spriteRend.enabled = false;
        cooldownTimer = 0;    
    }
}
