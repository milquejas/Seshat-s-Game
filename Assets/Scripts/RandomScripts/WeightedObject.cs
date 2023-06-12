using UnityEngine;

public class WeightedObject : MonoBehaviour
{
    [SerializeField]
    private int weight;
    [SerializeField]
    private string itemType;

    public int Weight { get { return weight; } }
    public string ItemType { get { return itemType; } }
}
