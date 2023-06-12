using UnityEngine;

public class QuestNavigator : MonoBehaviour
{
    [SerializeField]
    private Transform questTarget;
    [SerializeField]
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        UpdateNavigationLine();
    }

    private void UpdateNavigationLine()
    {
        if (questTarget != null)
        {
            Vector3[] positions = { transform.position, questTarget.position };
            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
