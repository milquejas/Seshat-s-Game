using UnityEngine;

public class FruitbasketDragAndDrop : MonoBehaviour
{
    private static bool isDraggingEnabled = true;

    private bool isDragging = false;
    private Vector2 startPosition;
    private Vector2 offset;
    private bool wasNewFruitCreated = false;  // Uusi muuttuja

    public GameController gameController;
    public GameObject[] fruitPrefabs;  // Hedelm‰prefabit
    public int fruitIndex;

    void Start()
    {
        // GameController and fruitIndex should be set in the inspector for each fruit
    }

    void OnMouseDown()
    {
        if (isDraggingEnabled)
        {
            isDragging = true;
            startPosition = transform.position;
            offset = startPosition - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CreateNewFruit();  // Luodaan uusi hedelm‰ heti, kun alkuper‰ist‰ aletaan raahata
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
        wasNewFruitCreated = false;  // Nollataan muuttuja seuraavaa raahausta varten
        if (!this.gameObject.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Basket").GetComponent<Collider2D>()))
        {
            transform.position = startPosition;
        }
    }

    void OnMouseEnter()
    {
        gameController.ShowTooltip(fruitIndex);
    }

    void OnMouseExit()
    {
        gameController.HideTooltip();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            gameController.AddFruitToBasket(fruitIndex);
            gameObject.SetActive(false);
        }
    }

    public static void DisableDragging()
    {
        isDraggingEnabled = false;
    }

    public static void EnableDragging()
    {
        isDraggingEnabled = true;
    }

    private void CreateNewFruit()
    {
        if (gameController.GetFruitQuantity(fruitIndex) > 0)
        {
            // Luodaan uusi hedelm‰
            GameObject newFruit = Instantiate(fruitPrefabs[fruitIndex], startPosition, Quaternion.identity);
            // Asetetaan uudelle hedelm‰lle oikea GameController ja hedelm‰n indeksi
            newFruit.GetComponent<FruitbasketDragAndDrop>().gameController = gameController;
            newFruit.GetComponent<FruitbasketDragAndDrop>().fruitIndex = fruitIndex;
        }
        else
        {
            // Jos hedelm‰‰ ei ole j‰ljell‰, luodaan tumma siluetti
            GameObject emptyFruit = Instantiate(fruitPrefabs[fruitIndex], startPosition, Quaternion.identity);
            emptyFruit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            // Poistetaan DragAndDrop-komponentti, jotta hedelm‰‰ ei voi en‰‰ raahata
            Destroy(emptyFruit.GetComponent<FruitbasketDragAndDrop>());
        }
    }
}
