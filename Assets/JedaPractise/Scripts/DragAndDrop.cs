using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool dragging;

    // When the mouse button is pressed, calculate the object's position and offset
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        dragging = true;
    }

    // Move the object when the mouse is being dragged
    void OnMouseDrag()
    {
        if (!dragging) return;
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
        transform.position = cursorPosition;
    }

    // Stop dragging the object when the mouse button is released
    void OnMouseUp()
    {
        dragging = false;
    }
}
