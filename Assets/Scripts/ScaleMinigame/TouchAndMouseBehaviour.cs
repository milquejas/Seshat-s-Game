using UnityEngine;

/* 
 * Scale minigame mouse/touch usage
*/

public class TouchAndMouseBehaviour : MonoBehaviour
{
    [SerializeField] private float interactionCircleSize;
    [SerializeField] private float draggingSpeed;
    private bool thisTouchInteracting;

    private Transform InteractTarget;
    private DraggableItem targetDragItem;
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
                if (!thisTouchInteracting)
                    InteractTarget = InteractSystem.TryToInteract(touchPosition, interactionCircleSize);

                if (InteractTarget != null)
                {
                    thisTouchInteracting = true;
                    if (InteractTarget.TryGetComponent<DraggableItem>(out DraggableItem target))
                    {
                        targetDragItem = target;
                        targetDragItem.StartDragging(true);
                    }
                }
                break;

            case TouchPhase.Moved:
                if (thisTouchInteracting)
                {
                    //InteractTarget.transform.position = touchPosition;

                    //targetDragItem.transform
                    Vector2 velocityDirection = new Vector2(touchPosition.x - targetDragItem.transform.position.x, touchPosition.y - targetDragItem.transform.position.y);
                    targetDragItem.weightedItem.RBody.velocity = velocityDirection * draggingSpeed;
                }
                break;

            case TouchPhase.Stationary:

                break;

            case TouchPhase.Ended:
                thisTouchInteracting = false;

                if (targetDragItem!= null)
                {
                    targetDragItem.StartDragging(false);
                }
                    

                break;
        }
    }
}
