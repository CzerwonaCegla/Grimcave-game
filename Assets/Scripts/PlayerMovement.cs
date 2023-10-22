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
        Movement();
    }

    private void Movement()
    {
        if(isDashing) 
        {
            return;
        }

        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            moveDir = -1f;
            rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);
        }

        //Move right
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = 1f;
            rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0f)
        {
            jumpDir = new Vector2(0f, 1f);
            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (!Input.anyKey)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    //Dash code
    private IEnumerator Dash()
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