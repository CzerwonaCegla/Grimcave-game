using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveDir, jumpDir;
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float jumpForce = 10;
    Rigidbody2D rb;

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
        if (Input.GetKey(KeyCode.A))
        {
            moveDir = new Vector2(-1, 0);
            transform.Translate(moveDir * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = new Vector2(1, 0);
            transform.Translate(moveDir * Time.deltaTime * movementSpeed);
        }
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
