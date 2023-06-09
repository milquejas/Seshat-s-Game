using UnityEngine;

public class FruitbasketDragAndDrop : MonoBehaviour
{
    private static bool isDraggingEnabled = true;
    private bool isMouseOver = false;
    private bool isDragging = false;
    private Vector2 startPosition;
    private Vector2 offset;

    public GameController gameController;
    public GameObject[] fruitPrefabs;
    public int fruitIndex;

    private bool isCreatingNewFruit = false; // Uusi muuttuja
    private float tooltipDelay = 0.1f; // Viive ennen tooltipin näyttämistä

    void Start()
    {
    }

    void OnMouseDown()
    {
        if (isDraggingEnabled)
        {
            isDragging = true;
            startPosition = transform.position;
            offset = startPosition - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isCreatingNewFruit = true; // Asetetaan uusi muuttuja todeksi
            CreateNewFruit();
            isCreatingNewFruit = false; // Asetetaan uusi muuttuja epätodeksi
          
        }
    }


    void OnMouseDrag()
    {
        if (isDragging)
        {
            gameController.HideTooltip();
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
        gameController.HideTooltip();
    }

    void OnMouseEnter()
    {
        isMouseOver = true;
        if (!isDragging && !isCreatingNewFruit) // Älä näytä tooltipia, jos objektia raahataan tai uutta hedelmää luodaan
        {
            Invoke(nameof(ShowTooltip), tooltipDelay);
        }
    }

    void OnMouseExit()
    {
        isMouseOver = false;
        if (!isDragging || !isDraggingEnabled)
        {
            CancelInvoke(nameof(ShowTooltip)); // Peruuta tooltipin näyttäminen, jos hiiri poistuu hedelmän päältä ennen viiveen loppumista
            gameController.HideTooltip();
        }
    }

    void ShowTooltip()
    {
        gameController.ShowTooltip(fruitIndex);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            gameController.AddFruitToBasket(fruitIndex);
            gameObject.SetActive(false);
            gameController.FruitInBasket();
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
            GameObject newFruit = Instantiate(fruitPrefabs[fruitIndex], startPosition, Quaternion.identity);
            newFruit.GetComponent<FruitbasketDragAndDrop>().gameController = gameController;
            newFruit.GetComponent<FruitbasketDragAndDrop>().fruitIndex = fruitIndex;
        }
        else
        {
            GameObject emptyFruit = Instantiate(fruitPrefabs[fruitIndex], startPosition, Quaternion.identity);
            emptyFruit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            Destroy(emptyFruit.GetComponent<FruitbasketDragAndDrop>());
        }
    }
}
