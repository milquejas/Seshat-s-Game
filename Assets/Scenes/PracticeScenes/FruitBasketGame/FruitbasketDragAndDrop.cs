using System.Collections;
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
   

    private Vector2 previousMousePosition;
    private float throwForce = 2000f;
    private float torqueForce = 100f;
    private bool isInBasket = false;  // Uusi muuttuja
    private bool isInInventory = true;
    private Quaternion originalRotation;


    void Start()
    {
        originalRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        if (isDraggingEnabled && !isInBasket && isInInventory)
        {
            isDragging = true;
            startPosition = transform.position;
            offset = startPosition - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isCreatingNewFruit = true; // Asetetaan uusi muuttuja todeksi
            CreateNewFruit();
            isCreatingNewFruit = false; // Asetetaan uusi muuttuja epätodeksi
            GetComponent<Rigidbody2D>().isKinematic = true;
            previousMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseDrag()
    {
        if (isDragging && !isInBasket)
        {
            gameController.HideTooltip();
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 newPosition = currentMousePosition + offset;
            transform.position = newPosition;
            previousMousePosition = currentMousePosition;
        }
    }

    void OnMouseUp()
    {
        if (isDragging && !isInBasket)
        {
            isDragging = false;
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 throwDirection = currentMousePosition - previousMousePosition;
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().AddForce(throwDirection * throwForce);

            Vector2 grabOffset = (Vector2)transform.position - currentMousePosition;
            float torque = Vector3.Cross(grabOffset, throwDirection.normalized).z;
            GetComponent<Rigidbody2D>().AddTorque(torque * torqueForce);

            StartCoroutine(ResetFruitPositionWithDelay());  // Käynnistä Coroutine
        }
    }

    void OnMouseEnter()
    {
        isMouseOver = true;
        if (!isDragging && !isCreatingNewFruit) // Älä näytä tooltipia, jos objektia raahataan tai uutta hedelmää luodaan
        {
            ShowTooltip();
        }
    }

    void OnMouseExit()
    {
        isMouseOver = false;
        gameController.HideTooltip();  // Piilota tooltip
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Inventory"))
        {
            isInInventory = false; // Asetetaan hedelmä pois inventaarion alueelta
            gameController.HideTooltip(); // Piilota tooltip, kun hedelmä poistuu inventaarion alueelta
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
            gameObject.layer = LayerMask.NameToLayer("InBasket");
            isInBasket = true;  // Aseta uusi muuttuja todeksi
            gameController.FruitInBasket();

            StopCoroutine(ResetFruitPositionWithDelay());  // Katkaise Coroutine
        }
        else if (other.gameObject.CompareTag("Inventory"))
        {
            isInInventory = true; // Asetetaan hedelmä inventaarion alueelle
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
            newFruit.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            GameObject emptyFruit = Instantiate(fruitPrefabs[fruitIndex], startPosition, Quaternion.identity);
            emptyFruit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            Destroy(emptyFruit.GetComponent<FruitbasketDragAndDrop>());
        }
    }

    IEnumerator ResetFruitPositionWithDelay()
    {
        yield return new WaitForSeconds(2);  // Odotetaan 2 sekuntia

        if (!isInBasket) // Tarkistetaan, ettei hedelmä ole korissa
        {
            ResetFruitPosition();  // Kutsutaan ResetFruitPosition-metodia
        }
    }

    private void ResetFruitPosition()
    {
        // Nollaa kaikki voimat
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;

        transform.position = startPosition;
        transform.rotation = originalRotation;
        GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        isInBasket = false;
    }
}
