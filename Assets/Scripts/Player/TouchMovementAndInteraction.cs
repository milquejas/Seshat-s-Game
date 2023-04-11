using UnityEngine;

/* 
 * Click -> check if interactable triggered -> check if in range -> interact -> no movement 
 * Otherwise movement
 * Mouse and touch based joystick movement. 
 * 
*/

public class TouchMovementAndInteraction : MonoBehaviour
{
    private Vector2 touchStartPosition, movedPosition, movementDirection;

    [SerializeField] private float minimumMove, moveSpeed, maxMoveLength, interactionCircleSize;

    [SerializeField] private LineRenderer bowGuideLine;

    public bool disableTouch;

    private bool thisTouchInteracting;

    private Rigidbody2D rbody;

    void Start()
    {
        InteractSystem.InitContactFilters();

        rbody = GetComponent<Rigidbody2D>();
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
    }

    private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase)
    {
        if (disableTouch) return;

        switch (touchPhase)
        {
            case TouchPhase.Began:
                if (InteractSystem.TryToInteract(touchPosition, interactionCircleSize))
                {
                    thisTouchInteracting = true;
                    rbody.velocity = Vector2.zero;
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
            rbody.velocity = Vector2.ClampMagnitude(movementDirection, maxMoveLength) * moveSpeed;
        }
    }

    void CheckIfInteracting(Vector2 touchPosition)
    {

    }
}
