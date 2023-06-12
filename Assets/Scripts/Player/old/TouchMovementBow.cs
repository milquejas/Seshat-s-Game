using UnityEngine;

/* 
 *
 * Touch/click to stop moving, pulling gives velocity to opposite direction when released. 
 * Click object -> during that click can't move and doesnt show line
 * Next click moves object
 *  
*/

public class TouchMovementBow : MonoBehaviour
{
    private Vector2 touchStartPosition, movementDirection;

    [SerializeField] private float minimumMove, moveSpeed, maxBowLength;
    [SerializeField] private LineRenderer bowGuideLine;
    [SerializeField] private float tapInteractCircleSize;

    private Rigidbody2D playerRigidbody;

    public bool CantMove;



    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        bowGuideLine.enabled = false;
    }

    void Update()
    {

        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleMoveTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleMoveTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleMoveTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleMoveTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }
    }

    private void HandleMoveTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase)
    {
        if (CantMove) return;

        switch (touchPhase)
        {
            case TouchPhase.Began:
                // if hit grabbable, stop moving


                // spawn something here to show origin
                bowGuideLine.SetPosition(0, touchPosition);
                bowGuideLine.SetPosition(1, touchPosition);
                bowGuideLine.enabled = true;

                touchStartPosition = touchPosition;

                playerRigidbody.velocity = Vector2.zero;
                break;

            case TouchPhase.Moved:



                bowGuideLine.SetPosition(1, touchPosition);

                break;

            case TouchPhase.Stationary:

                break;

            case TouchPhase.Ended:

                Rigidbody2D rb = playerRigidbody;



                movementDirection = touchStartPosition - touchPosition;

                if (Mathf.Abs(movementDirection.x) > minimumMove || Mathf.Abs(movementDirection.y) > minimumMove)
                {
                    rb.velocity = Vector2.ClampMagnitude(movementDirection, maxBowLength) * moveSpeed;
                }

                // disable line
                bowGuideLine.enabled = false;
                break;
        }
    }
}
