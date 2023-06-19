using UnityEngine;

/* 
 * Press screen -> move player towards spot
 * clamped movementDirection
 * 
*/

[RequireComponent(typeof(Rigidbody2D))]
public class TouchMovementAndInteraction : MonoBehaviour, IPlayerTouch
{
    [SerializeField] private float minimumMove, movementSpeed, interactionCircleSize;
    [SerializeField] private LevelSpawnSO spawnPoint;
    [SerializeField] private Transform spriteAndAnimationChild;

    private Vector2 movementDirection, playerPosition;
    private bool disableMovement;
    private bool thisTouchInteracting;
    private bool isTouchMoving;
    private bool isFacingLeft;

    public Rigidbody2D rb { get; private set; }
    private Animator anim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // setup spawn
        transform.position = spawnPoint.CurrentSpawnLocation;
        //Camera.main.transform.position = spawnPoint.CurrentSpawnLocation;
    }

    public void SetSpawnLocation()
    {
        spawnPoint.CurrentSpawnLocation = transform.position;
    }

    void Update()
    {
        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }

        // keyboard movement
        if (disableMovement) return;

        if (!isTouchMoving)
        {
            movementDirection = GetKeyboardMovement();
        }
    }

    private Vector2 GetKeyboardMovement()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void DisablePlayerMovement(bool disable)
    {
        disableMovement = disable;
        movementDirection = Vector2.zero;
    }


    private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase)
    {
        if (disableMovement)
        {
            return;
        }
        
        switch (touchPhase)
        {
            case TouchPhase.Began:
                if (InteractSystem.TryToInteract(touchPosition, interactionCircleSize))
                {
                    thisTouchInteracting = true;
                    rb.velocity = Vector2.zero;
                    return;
                }

                TouchBegin(touchPosition);
                break;

            case TouchPhase.Moved:
                if (thisTouchInteracting) return;

                TouchMoving(touchPosition);
                break;

            case TouchPhase.Stationary:
                if (thisTouchInteracting) return;

                TouchStationary(touchPosition);
                break;

            case TouchPhase.Ended:
                if (thisTouchInteracting)
                {
                    thisTouchInteracting = false;
                    return;
                }
                TouchEnding(touchPosition);
                break;
        }
    }

    private void TouchBegin(Vector2 touchPosition)
    {
        isTouchMoving = true;
        playerPosition = transform.position;
        movementDirection = touchPosition - playerPosition;
    }

    private void TouchMoving(Vector2 touchPosition)
    {
        playerPosition = transform.position;
        movementDirection = touchPosition - playerPosition;
    }

    private void TouchStationary(Vector2 touchPosition)
    {
        playerPosition = transform.position;
        movementDirection = touchPosition - playerPosition;
    }

    private void TouchEnding(Vector2 touchPosition)
    {
        movementDirection = Vector2.zero;
        isTouchMoving = false;
    }

    private void FixedUpdate()
    {
        if (disableMovement) return;

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Mathf.Abs(movementDirection.x) > minimumMove || Mathf.Abs(movementDirection.y) > minimumMove)
        {
            PlayerFlip();
            rb.velocity = movementDirection.normalized * movementSpeed;
        }
    }

    private void PlayerFlip()
    {
        if (movementDirection.x < 0 && isFacingLeft) return;
        if (movementDirection.x > 0 && !isFacingLeft) return;

        isFacingLeft = !isFacingLeft;
        spriteAndAnimationChild.localScale = new Vector3(spriteAndAnimationChild.localScale.x * -1, 1, 1);
    }
}
