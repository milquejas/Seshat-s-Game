using UnityEngine;

[CreateAssetMenu(fileName = "New weight item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public ItemType ItemName;
    public Sprite ItemImage;
    public int ItemWeight;
}

public enum ItemType
{
    Apple, Cantaloupe, Citrus, Grapes, Herbs, Olives, Onion, Orange, Pomegranate, Potato, Watermelon, Item
}