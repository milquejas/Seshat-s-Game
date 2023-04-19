using UnityEngine;
using TMPro;

public class ScaleController : MonoBehaviour
{
    public Transform LeftPan;
    public Transform RightPan;
    public TextMeshProUGUI BalanceText;

    private float _leftPanWeight;
    private float _rightPanWeight;

    private void Update()
    {
        if (Mathf.Abs(_leftPanWeight - _rightPanWeight) <= 0.01f)
        {
            BalanceText.text = "Vaaka on tasapainossa!";
        }
        else
        {
            BalanceText.text = "Vaaka ei ole tasapainossa!";
        }
    }

    public void AddWeightToPan(Transform pan, float weight)
    {
        if (pan == LeftPan)
        {
            _leftPanWeight += weight;
        }
        else if (pan == RightPan)
        {
            _rightPanWeight += weight;
        }
    }

    public void RemoveWeightFromPan(Transform pan, float weight)
    {
        if (pan == LeftPan)
        {
            _leftPanWeight -= weight;
        }
        else if (pan == RightPan)
        {
            _rightPanWeight -= weight;
        }
    }
}
