using UnityEngine;

/*
 * Setup item so it takes info from SO when enabled so it can be pooled
 * Spawning back to inventory if dropped outside the game. 
 * Animation when placed into inventory
 * Needs reference of coordinates of item slot in inventory? Or just fly into the middle of the inventory?
 * Draggable
*/

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class WeightedItem : MonoBehaviour
{
    public ItemSO Item;

    private SpriteRenderer itemImage;
    private Rigidbody2D rBody;

    private void OnEnable()
    {
        itemImage = GetComponent<SpriteRenderer>();
        itemImage.sprite = Item.ItemImage;
        rBody = GetComponent<Rigidbody2D>();
        rBody.mass = Item.ItemWeight / 10;
    }
}
