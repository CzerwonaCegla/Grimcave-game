using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 jumpDir = new Vector3(0f, 1f);
    private float moveDir = 1;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;

    Rigidbody2D rb;

    private bool goRight, goLeft, jump, dash, doNothing;

    [SerializeField] Transform groundDetector;
    [SerializeField] LayerMask groundLayer;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashDistance = 10f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing) { CheckKeys(); }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void CheckKeys()
    {
        //Detect go left
        goLeft = Input.GetKey(KeyCode.A);

        //Detect go right
        goRight = Input.GetKey(KeyCode.D);

        //Detect jump
        if (Input.GetKeyDown(KeyCode.Space)) { jump = true; };

        //Detect dash
        if (Input.GetKeyDown(KeyCode.LeftShift)) { dash = true; }

        //Do if no buttons pressed (to stop rb slide)
        doNothing = !Input.anyKey;
    }

    private void Movement()
    {
        //Move left
        if (goLeft)
        {
            goLeft = false;
            moveDir = -1f;
            rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);
        }

        //Move right
        if (goRight)
        {
            goRight = false;
            moveDir = 1f;
            rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);
        }

        //Jump
        if (jump && GroundCheck())
        {
            jump = false;
            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
        }

        //Dash
        if (dash && canDash)
        {
            dash = false;
            StartCoroutine(DashKor());
        }

        //Execute if no keys pressed
        if (doNothing)
        {
            doNothing = false;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    //Checks if player on ground layer
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundDetector.position, 0.1f, groundLayer);
    }

    //Dash code
    private IEnumerator DashKor()
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
}