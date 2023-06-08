using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private static bool isDraggingEnabled = true; // Lisätty staattinen muuttuja, joka määrittää, onko raahaus sallittu

    private bool isDragging = false;
    private Vector2 startPosition;
    private Vector2 offset;

    public GameController gameController;
    public int fruitIndex;

    void Start()
    {
        // GameController and fruitIndex should be set in the inspector for each fruit
    }

    void OnMouseDown()
    {
        if (isDraggingEnabled) // Tarkistetaan, onko raahaus sallittu
        {
            isDragging = true;
            startPosition = transform.position;
            offset = startPosition - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (!this.gameObject.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Basket").GetComponent<Collider2D>()))
        {
            transform.position = startPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            gameController.AddFruitToBasket(fruitIndex);
            gameObject.SetActive(false); // hide the fruit after it has been added to the basket
        }
    }

    public static void DisableDragging() // Uusi julkinen staattinen metodi, joka poistaa raahauksen käytöstä
    {
        isDraggingEnabled = false;
    }

    public static void EnableDragging() // Uusi julkinen staattinen metodi, joka sallii raahauksen
    {
        isDraggingEnabled = true;
    }
}