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
    private Animator anim;

    Rigidbody2D rb;

    private bool goRight, goLeft, jump, dash, doNothing;

    [SerializeField] Transform groundDetector;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] private float dashDistance = 10f;
    private bool canDash = true;
    private bool isDashing;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDashing) { CheckKeys(); }

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
            anim.SetBool("run", goLeft || goRight);
        }
        anim.SetBool("grounded", GroundCheck() != false);
        anim.SetBool("dash", isDashing != false);

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
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck()) { jump = true; };

        //Detect dash
        if (Input.GetKeyDown(KeyCode.LeftShift)) { dash = true; }

        if (!Input.anyKey)
        {
            doNothing = true;
        }
    }

    private void Movement()
    {
        //Execute if no keys pressed
        if (doNothing)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        //Move left
        if (goLeft)
        {
            moveDir = -1f;
            rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);
            goLeft = false;
        }

        //Move right
        if (goRight)
        {
            moveDir = 1f;
            rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);
            goRight = false;
        }

        //Jump
        if (jump)
        {
            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            jump = false;
        }

        //Dash
        if (dash && canDash)
        {
            dash = false;
            StartCoroutine(DashKor());
        }

        
    }

    //Checks if player on ground layer
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundDetector.position, 0.15f, groundLayer);
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