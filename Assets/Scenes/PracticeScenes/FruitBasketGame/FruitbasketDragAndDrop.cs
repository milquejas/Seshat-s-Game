using UnityEngine;

public class FruitbasketDragAndDrop : MonoBehaviour
{
    private static bool isDraggingEnabled = true; // Onko raahaaminen sallittu
    private bool isMouseOver = false; // Onko hiiri objektin p‰‰ll‰
    private bool isDragging = false; // Onko objektia raahattu
    private Vector2 startPosition; // Alkuper‰inen sijainti
    private Vector2 offset; // Hiiren sijainnin ja objektin sijainnin v‰linen ero

    public GameController gameController; // Viittaus peliohjaimen komponenttiin
    public GameObject[] fruitPrefabs; // Taulukko hedelm‰prefabeista
    public int fruitIndex; // Hedelm‰n indeksi

    private bool isCreatingNewFruit = false; // Uusi muuttuja
    private float tooltipDelay = 0.1f; // Viive ennen tooltipin n‰ytt‰mist‰

    private Rigidbody2D fruitRigidBody; // Hedelm‰n Rigidbody2D-komponentti

    private int fruitToDeduct = -1;  // Lis‰tty muuttuja
    private bool isFirstDrag = true; // Lis‰tty muuttuja
    private bool newFruitCreated = false; // Uusi muuttuja
    private bool isInsideGameArea = false; // Onko hedelm‰ pelialueen sis‰ll‰



    void Start()
    {
        fruitRigidBody = GetComponent<Rigidbody2D>(); // Hae Rigidbody2D-komponentti
    }

    void OnMouseDown()
    {
        if (isDraggingEnabled)
        {
            isDragging = true;
            startPosition = transform.position;
            offset = startPosition - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            fruitRigidBody.isKinematic = true;
            newFruitCreated = false; // Aseta uusi hedelm‰ luomattomaksi
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            gameController.HideTooltip();
            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPosition;
            if (!newFruitCreated && !isInsideGameArea) // Tarkista, onko uusi hedelm‰ jo luotu JA onko hedelm‰ pelialueella
            {
                CreateNewFruit(); // Luo uusi hedelm‰ vain, jos sit‰ ei ole viel‰ luotu JA hedelm‰ ei ole pelialueella
                newFruitCreated = true; // Aseta uusi hedelm‰ luoduksi
            }
        }
    }


    void OnMouseUp()
{
    if (isDragging)
    {
        isDragging = false; // Aseta raahaaminen pois p‰‰lt‰

        if (isTouchingBasket())
        {
            CreateNewFruit(); // Luo uusi hedelm‰, jos objekti koskettaa koria
        }

        gameController.HideTooltip(); // Piilota tooltip

        if (!isTouchingBasket())
        {
            fruitRigidBody.isKinematic = false; // Aseta Rigidbody ei-kinemaattiseksi, jos objekti ei kosketa koria
        }

        // Tarkista, onko hedelm‰n m‰‰r‰ nolla inventaariossa
        if(gameController.GetFruitQuantity(fruitIndex) == 0) 
        {
            gameController.UpdateInventoryTexts(); // P‰ivit‰ teksti‰, jos hedelm‰n m‰‰r‰ inventaariossa on nolla
        }
    }
}



    void OnMouseEnter()
    {
        isMouseOver = true; // Aseta hiiri objektin p‰‰ll‰

        if (!isDragging && !isCreatingNewFruit) // ƒl‰ n‰yt‰ tooltipia, jos objektia raahataan tai uutta hedelm‰‰ luodaan
        {
            Invoke(nameof(ShowTooltip), tooltipDelay); // N‰yt‰ tooltip viiveen j‰lkeen
        }

        // Tarkista, onko hiiri pelialueella
        Collider2D fruitGameAreaCollider = GameObject.FindGameObjectWithTag("FruitGameArea").GetComponent<Collider2D>(); // Hae pelialueen Collider2D-komponentti
        if (fruitGameAreaCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) // Tarkista, koskettaako hiiri pelialuetta
        {
            fruitRigidBody.isKinematic = false; // Aseta Rigidbody ei-kinemaattiseksi
        }
    }

    void OnMouseExit()
    {
        isMouseOver = false; // Aseta hiiri pois objektin p‰‰lt‰

        if (!isDragging || !isDraggingEnabled)
        {
            CancelInvoke(nameof(ShowTooltip)); // Peruuta tooltipin n‰ytt‰minen, jos hiiri poistuu hedelm‰n p‰‰lt‰ ennen viiveen loppumista
            gameController.HideTooltip(); // Piilota tooltip
        }
    }

    void ShowTooltip()
    {
        gameController.ShowTooltip(fruitIndex); // N‰yt‰ tooltip
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Basket") && !newFruitCreated) // Jos objekti koskettaa koria JA uutta hedelm‰‰ ei ole luotu
        {
            gameController.AddFruitToBasket(fruitIndex); // Lis‰‰ hedelm‰ koriin
            gameController.FruitInBasket(); // P‰ivit‰ peliohjainta hedelm‰n lis‰‰misen j‰lkeen
            newFruitCreated = true; // Aseta uusi hedelm‰ luoduksi
        }
        else if (other.gameObject.CompareTag("FruitGameArea")) // Jos objekti on pelialueella
        {
            isInsideGameArea = true; // Aseta isInsideGameArea true:ksi
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FruitGameArea")) // Jos objekti poistuu pelialueelta
        {
            isInsideGameArea = false; // Aseta isInsideGameArea false:ksi
        }
    }

    public static void DisableDragging()
    {
        isDraggingEnabled = false; // Poista raahaaminen k‰ytˆst‰
    }

    public static void EnableDragging()
    {
        isDraggingEnabled = true; // Salli raahaaminen
    }

    private void CreateNewFruit()
    {
        if (gameController.GetFruitQuantity(fruitIndex) > 1) // Tarkista, onko hedelmi‰ j‰ljell‰ inventaarissa
        {
            GameObject newFruit = Instantiate(fruitPrefabs[fruitIndex], startPosition, Quaternion.identity); // Luo uusi hedelm‰
            newFruit.GetComponent<FruitbasketDragAndDrop>().gameController = gameController; // Aseta peliohjaimen viittaus uudelle hedelm‰lle
            newFruit.GetComponent<FruitbasketDragAndDrop>().fruitIndex = fruitIndex; // Aseta hedelm‰n indeksi uudelle hedelm‰lle

            gameController.AddFruitToBasket(fruitIndex); // Lis‰‰ uusi hedelm‰ koriin ja v‰henn‰ hedelm‰n m‰‰r‰‰ inventaarissa
        }
        else
        {
            GameObject emptyFruit = Instantiate(fruitPrefabs[fruitIndex], startPosition, Quaternion.identity); // Luo tyhj‰ hedelm‰
            emptyFruit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1); // Aseta tyhj‰n hedelm‰n v‰ri harmaaksi
            Destroy(emptyFruit.GetComponent<FruitbasketDragAndDrop>()); // Poista raahaaminen tyhj‰lt‰ hedelm‰lt‰
        }
    }


    private bool isTouchingBasket()
    {
        return this.gameObject.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Basket").GetComponent<Collider2D>()); // Tarkista, koskettaako objekti koria
    }
}
