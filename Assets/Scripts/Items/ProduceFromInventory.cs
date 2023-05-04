using UnityEngine;

/*
 * How items inside scale minigame work when dragged out of inventory
 *
*/

public class ProduceFromInventory : MonoBehaviour
{
    private void Start()
    {
        GetComponent<WeightedItem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OutOfBounds"))
        {
            AddBackIntoInventory();
        }
    }

    private void AddBackIntoInventory()
    {
        // add to inventory 
        // Destroy(gameObject);
        Debug.Log("set position to 9999, 9999 or something, then enabled false");
    }
}