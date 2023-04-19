using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem2D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float Weight;
    private Camera _mainCamera;
    private Vector3 _startPosition;
    private Transform _startParent;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _startParent = transform.parent;
        transform.SetParent(_mainCamera.transform);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPoint = new Vector3(eventData.position.x, eventData.position.y, _startPosition.z);
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPoint);
        worldPosition.z = 0;
        transform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_startParent);
        transform.position = _startPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Scale"))
        {
            ScaleController scaleController = hit.collider.GetComponent<ScaleController>();
            if (transform.parent != null)
            {
                scaleController.RemoveWeightFromPan(transform.parent, Weight);
            }

            if (hit.collider.transform == scaleController.LeftPan || hit.collider.transform == scaleController.RightPan)
            {
                transform.SetParent(hit.collider.transform);
                scaleController.AddWeightToPan(hit.collider.transform, Weight);
            }
            else
            {
                transform.SetParent(null);
            }
        }
    }
}
