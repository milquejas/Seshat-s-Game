using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance; // Singleton instance
    public float moveSpeed = 5f;
    private Vector2 movement;

    public bool canMove = true; // Whether the player can move

    private void Awake()
    {
        // Setup singleton instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Only accept movement input if the player can move
        if (canMove)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movement = AdjustMovementForIsometric(input);
        }
    }


    void FixedUpdate()
    {
        // Käytä muutettua liikettä pelaajan sijainnin päivittämiseen
        transform.position = new Vector2(transform.position.x + movement.x * moveSpeed * Time.deltaTime, transform.position.y + movement.y * moveSpeed * Time.deltaTime);
    }

    private Vector2 AdjustMovementForIsometric(Vector2 input)
    {
        // Säädä syöte sopimaan isometriseen näkymään
        Vector2 adjustedInput = new Vector2((input.x - input.y) / 2, (input.x + input.y) / 2);
        return adjustedInput.normalized;
    }
}
