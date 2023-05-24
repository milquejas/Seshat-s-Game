using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool dragging;
    private Transform originalParent;
    public Transform draggableObjectsParent;

    private void Update()
    {
        if (OnMouseOver() && !dragging && Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (dragging && Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    private bool OnMouseOver()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        Collider2D collider = GetComponent<Collider2D>();

        return collider.OverlapPoint(mouseWorldPosition);
    }

    private void StartDragging()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        dragging = true;
        originalParent = transform.parent;
        transform.SetParent(draggableObjectsParent);
    }

    // Move the object when the mouse is being dragged
    void FixedUpdate()
    {
        if (!dragging) return;

        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
        transform.position = cursorPosition;
    }

    // Stop dragging the object when the mouse button is released and attach it to the cup if dropped over a cup
    void StopDragging()
    {
        dragging = false;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        bool isCup = false;
        Transform newParent = null;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("LeftCup") || hit.collider.CompareTag("RightCup"))
            {
                newParent = hit.collider.transform;
                isCup = true;
                break;
            }
        }

        if (isCup)
        {
            transform.SetParent(newParent);
        }
        else
        {
            transform.SetParent(draggableObjectsParent);
        }
    }
}
