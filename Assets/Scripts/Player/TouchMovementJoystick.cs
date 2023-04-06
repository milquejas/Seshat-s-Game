using UnityEngine;

/* 
 *
 * Mouse and touch based joystick movement. 
 * 
*/

public class TouchMovementJoystick : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchStartPosition, movedPosition, touchEndPosition, movementDirection;

    [SerializeField] private float minimumMove, moveSpeed, maxMoveLength;

    private Rigidbody2D rbody;

    
    void Start()
    {
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

    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {
        switch (touchPhase)
        {
            case TouchPhase.Began:
                touchStartPosition = touchPosition;
                break;

            case TouchPhase.Moved:

                movedPosition = new Vector2(touchPosition.x, touchPosition.y);
                movementDirection = movedPosition - touchStartPosition;
                

                if(Mathf.Abs(movementDirection.x) > minimumMove || Mathf.Abs(movementDirection.y) > minimumMove)
                {
                    Debug.Log(movementDirection);
                    rbody.velocity = Vector2.ClampMagnitude(movementDirection, maxMoveLength) * moveSpeed;
                }
                
                break;

            case TouchPhase.Stationary:

                if (Mathf.Abs(movementDirection.x) > minimumMove || Mathf.Abs(movementDirection.y) > minimumMove)
                {
                    Debug.Log(movementDirection);
                    rbody.velocity = Vector2.ClampMagnitude(movementDirection, maxMoveLength) * moveSpeed;
                }

                break;

            case TouchPhase.Ended:
                movementDirection = Vector2.zero;
                Debug.Log("ended");
                break;
        }
    }
}
