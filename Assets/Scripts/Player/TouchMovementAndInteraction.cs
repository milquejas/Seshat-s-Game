using UnityEngine;

/* 
 * check if interactable triggered -> check if in player range -> interact -> no movement 
 * Otherwise movement
 * Mouse and touch based joystick movement. 
 * 
*/

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class TouchMovementAndInteraction : MonoBehaviour
{
    private Vector2 touchStartPosition, movedPosition, movementDirection;

    [SerializeField] private float minimumMove, moveSpeedMultiplier, interactionCircleSize, playerInteractionDistance, runSpeedMultiplier;
    [field: SerializeField] public float MaxMoveSpeed { get; private set; }

    [SerializeField] private LineRenderer bowGuideLine;

    public bool disableMovement;

    private bool thisTouchInteracting;
    private Vector2 movement;
    public Rigidbody2D rb { get; private set; }


    private Animator anim;
    //private bool canMove = true; // Added line

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        if (disableMovement) return;

        movementDirection = new Vector2(movement.x = Input.GetAxisRaw("Horizontal"), movement.y = Input.GetAxisRaw("Vertical"));
        movementDirection *= runSpeedMultiplier;
        MovePlayer();
    }

    public void FreezePlayer() // Added method
    {
        disableMovement = true;
    }

    public void UnfreezePlayer() // Added method
    {
        disableMovement = false;
    }

    private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase)
    {
        if (disableMovement)
        {
            bowGuideLine.enabled = false;
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
                TouchEnd(touchPosition);
                break;
        }
    }
    private void TouchBegin(Vector2 touchPosition)
    {
        touchStartPosition = touchPosition;
        movedPosition = Vector2.zero;

        // line guide
        bowGuideLine.SetPosition(0, touchPosition);
        bowGuideLine.SetPosition(1, touchPosition);
        bowGuideLine.enabled = true;
    }

    private void TouchMoving(Vector2 touchPosition)
    {
        // update movement direction
        movedPosition = new Vector2(touchPosition.x, touchPosition.y);
        movementDirection = movedPosition - touchStartPosition;

        MovePlayer();

        // line guide
        bowGuideLine.SetPosition(1, touchPosition);
    }

    private void TouchStationary(Vector2 touchPosition)
    {
        MovePlayer();
    }

    private void TouchEnd(Vector2 touchPosition)
    {
        // disable line
        bowGuideLine.enabled = false;
    }

    private void MovePlayer()
    {
        if (Mathf.Abs(movementDirection.x) > minimumMove || Mathf.Abs(movementDirection.y) > minimumMove)
        {
            
            // TODO animate here
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(movementDirection * moveSpeedMultiplier, MaxMoveSpeed);
    }
}
