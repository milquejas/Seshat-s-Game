using UnityEngine;

/* 
 * Scale minigame mouse/touch usage
*/

public class TouchAndMouseBehaviour : MonoBehaviour
{
    private Vector2 touchStartPosition, movedPosition, movementDirection;
    [SerializeField] private float interactionCircleSize;
    private bool thisTouchInteracting;

    private Transform InteractTarget;
    [field: SerializeField] public bool ControlDisabled { get; set; }

    void Start()
    {
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
        if (ControlDisabled)
        {
            return;
        }

        switch (touchPhase)
        {
            case TouchPhase.Began:
                InteractTarget = InteractSystem.TryToInteract(touchPosition, interactionCircleSize);
                if (InteractTarget != null)
                {
                    Debug.Log("test");
                    thisTouchInteracting = true;
                }
                break;

            case TouchPhase.Moved:
                if (thisTouchInteracting)
                {
                    Debug.Log("test");
                    InteractTarget.transform.position = touchPosition;
                }
                break;

            case TouchPhase.Stationary:

                break;

            case TouchPhase.Ended:
                thisTouchInteracting = false;
                break;
        }
    }
}
