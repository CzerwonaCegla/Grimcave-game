using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveDir, jumpDir;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    Rigidbody2D rb;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashDistance = 10f;
    private float dashTime = 0.4f;
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
            moveDir = new Vector2(-1, 0);
            transform.Translate(moveDir * Time.deltaTime * movementSpeed);
        }

        //Move right
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = new Vector2(1, 0);
            transform.Translate(moveDir * Time.deltaTime * movementSpeed);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb.velocity.y == 0)
            {
                jumpDir = new Vector2(0, 1);
                rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
            }
        }

        //Dash
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
        }
    }

    //Dash code
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(moveDir.x * dashDistance, 0f);
        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        
    }
}
