using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    //Color colour; //placeholder do animacji
    BoxCollider2D colider;
    private bool CRrunning;
    private SpriteRenderer spriteRenderer;

    public Sprite spriteStage1;
    public Sprite spriteStage2;
    public Sprite spriteStage3;
    public Sprite spriteStage4;
    public Sprite spriteStage5;
    public Sprite spriteStage6;
    public Sprite spriteStage7;

    [Space(10)]
    [SerializeField] private AudioSource soundStage1;
    [SerializeField] private AudioSource soundStage2;
    [SerializeField] private AudioSource soundStage3;
    void Start()
    {
        colider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colider.enabled = true;
        spriteRenderer.sprite = spriteStage1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CRrunning)
        {
            StartCoroutine(PlatformDestruction());
        }
    }

    private IEnumerator PlatformDestruction()
    {
        CRrunning = true;

        // stage 2
        spriteRenderer.sprite = spriteStage2;
        soundStage1.Play();
        yield return new WaitForSeconds(0.5f);

        // stage 3
        spriteRenderer.sprite = spriteStage3;
        soundStage2.Play();
        yield return new WaitForSeconds(0.5f);

        // stage 4 - platform break1
        soundStage3.Play();
        spriteRenderer.sprite = spriteStage4;
        colider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        // stage 5 - platform break2
        spriteRenderer.sprite = spriteStage5;
        yield return new WaitForSeconds(0.1f);

        // stage 6 - platform break3
        spriteRenderer.sprite = spriteStage6;
        yield return new WaitForSeconds(0.1f);

        // stage 7 - platform break4
        spriteRenderer.sprite = spriteStage7;
        yield return new WaitForSeconds(4f);

        // stage 8 - platform respawn1
        spriteRenderer.sprite = spriteStage6;
        yield return new WaitForSeconds(0.1f);

        // stage 9 - platform respawn2
        spriteRenderer.sprite = spriteStage5;
        yield return new WaitForSeconds(0.1f);

        // stage 10 - platform respawn3
        spriteRenderer.sprite = spriteStage4;
        yield return new WaitForSeconds(0.1f);

        // stage 11 - platform respawn
        spriteRenderer.sprite = spriteStage1;
        colider.enabled = true;

        CRrunning = false;
    }
}
