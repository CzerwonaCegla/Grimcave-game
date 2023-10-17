using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveDir, jumpDir;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float dashDistance = 15f;
    Rigidbody2D rb;
    private bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
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
    }

    void Jump()
    {
        //Jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.velocity.y == 0)
            {
                jumpDir = new Vector2(0, 1);
                rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
