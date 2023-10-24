using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 jumpDir;
    private float moveDir;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;

    Rigidbody2D rb;

    private bool goLeft, goRight, jump, dash, doNothing;

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
        CheckKeys();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void CheckKeys()
    {
        //Return if dashing
        if (isDashing)
        {
            return;
        }

        //Detect go left
        if (Input.GetKey(KeyCode.A))
        {
            goLeft = true;
        }

        //Detect go right
        if (Input.GetKey(KeyCode.D))
        {
            goRight = true;
        }

        //Detect jump
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0f)
        {
            jump = true;
        }

        //Detect dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dash = true;
        }

        //Do if no buttons pressed (to stop rb slide)
        else if (!Input.anyKey)
        {
            doNothing = true;
        }
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
        if (jump)
        {
            jump = false;
            jumpDir = new Vector2(0f, 1f);
            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
        }

        //Dash
        if (dash)
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