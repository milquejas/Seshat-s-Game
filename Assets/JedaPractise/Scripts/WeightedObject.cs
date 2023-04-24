using UnityEngine;

public class WeightedObject : MonoBehaviour
{
    [SerializeField]
    private int weight;

    public int Weight { get { return weight; } }
}
