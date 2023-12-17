using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    [SerializeField] private ParticleSystem movementParticles;
    private bool jump, dash;
    private int moveDir = 1;
    [Space(10)]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float moveSmooth = 0.05f;
    Vector3 Velo = Vector3.zero;

    Rigidbody2D rb;
    Animator anim;

    [Space(10)]
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing;
    private bool damaged = false;

    [Space(10)]
    [SerializeField] Transform groundDetector;
    [SerializeField] LayerMask groundLayer;

    [Space(10)]
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;
    [SerializeField] private AudioSource runSoundEffect;
    [SerializeField] private AudioSource damageSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        var em = movementParticles.emission;
        em.rateOverTime = 0;
    }

    private void Update()
    {
        if (!damaged)
        {
            if (!isDashing)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    moveDir = 1;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    moveDir = -1;
                }
                else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) { moveDir = 0; }
                else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) { moveDir = 0; }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    dash = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jump = true;
                }
            }
        }

            
        if (moveDir > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDir < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            ;
        }

        if (jump == false)
        {
            anim.SetBool("run", moveDir != 0f);
        }
        anim.SetBool("grounded", GroundCheck() != false);
        anim.SetBool("dash", isDashing != false);

        if (moveDir != 0f && GroundCheck())
        {
            var em = movementParticles.emission;
            em.rateOverTime = 20;
        }
        else
        {
            var em = movementParticles.emission;
            em.rateOverTime = 0;
        }
    }

    private void FixedUpdate()
    {
        Movement(moveDir, dash, jump);
        jump = false;
        dash = false;

        if (moveDir != 0 && GroundCheck())
        {
            if (!runSoundEffect.isPlaying)
            {
                runSoundEffect.Play();
            }
        }
        else
        {
            runSoundEffect.Stop();
        }
    }

    private void Movement(float moveDir, bool dash, bool jump)
    {
        if (!damaged)
            {
            if (jump && GroundCheck())
            {
                //Debug.Log("Jumped");
                jumpSoundEffect.Play();
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            }

            if (dash && canDash && moveDir != 0)
            {
                //Debug.Log("Dashed");
                dashSoundEffect.Play();
                StartCoroutine(DashCor());
            }
            if (!isDashing)
            {
                Vector3 targetVelocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velo, moveSmooth);
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private IEnumerator DashCor()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(moveDir * dashDistance, 0f);
        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        GetComponent<Renderer>().material.color = Color.cyan;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundDetector.position, 0.15f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Traps") && !damaged)
        {
            StartCoroutine(TriggerDamage());
        }
    }

    private IEnumerator TriggerDamage()
    {
        damaged = true;
        damageSoundEffect.Play();
        anim.SetBool("damaged", damaged);

        // Wait for a few seconds
        yield return new WaitForSeconds(0.5f); // Change the time as needed

        damaged = false;
        anim.SetBool("damaged", damaged);
    }
}
