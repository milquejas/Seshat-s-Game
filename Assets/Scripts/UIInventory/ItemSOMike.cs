using UnityEngine;


[CreateAssetMenu(fileName = "New Item" ,menuName = "Item/Create New Item")]
public class ItemSOMike : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite itemImage;
    public ItemTypeMike itemType;

    public enum ItemTypeMike
    {
        Apple,
        Citrus
    }
}

