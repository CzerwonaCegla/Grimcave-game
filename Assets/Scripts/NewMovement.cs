using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
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

    [Space(10)]
    [SerializeField] Transform groundDetector;
    [SerializeField] LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
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

    private void FixedUpdate()
    {
        Movement(moveDir, dash, jump);
        jump = false;
        dash = false;
    }

    private void Movement(float moveDir, bool dash, bool jump)
    {
        if (jump && GroundCheck())
        {
            Debug.Log("Jumped");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (dash && canDash && moveDir!=0)
        {
            Debug.Log("Dashed");
            StartCoroutine(DashCor());
        }
        if (!isDashing)
        {
            Vector3 targetVelocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velo, moveSmooth);
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
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundDetector.position, 0.15f, groundLayer);
    }
}
