using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private bool canMove = true;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void FreezePlayer()
    {
        canMove = false;
    }

    public void UnfreezePlayer()
    {
        canMove = true;
    }
}
