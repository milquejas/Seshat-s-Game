using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerController class manages the movement of the player character in the game.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // The speed at which the player character moves.
    private Rigidbody2D rb; // The Rigidbody2D component of the player character.
    private bool canMove = true; // A boolean flag to check whether the player character can move.
    private Vector2 movement; // The direction and magnitude of the player character's movement.

    // Start method is called before the first frame update.
    private void Start()
    {
        // Getting the Rigidbody2D component of the player character.
        rb = GetComponent<Rigidbody2D>();
    }

    // Update method is called once per frame.
    private void Update()
    {
        // Getting the horizontal and vertical input values.
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Creating a new Vector2 for the movement direction and normalizing it.
        movement = new Vector2(moveX, moveY).normalized;
    }

    // FixedUpdate method is called every fixed framerate frame.
    private void FixedUpdate()
    {
        // Checking if the player character can move.
        if (canMove)
        {
            // Applying velocity to the Rigidbody2D of the player character.
            rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }
        else
        {
            // Setting the velocity of the Rigidbody2D to zero.
            rb.velocity = Vector2.zero;
        }
    }

    // FreezePlayer method sets canMove to false, stopping the player character's movement.
    public void FreezePlayer()
    {
        canMove = false;
    }

    // UnfreezePlayer method sets canMove to true, enabling the player character's movement.
    public void UnfreezePlayer()
    {
        canMove = true;
    }
}
