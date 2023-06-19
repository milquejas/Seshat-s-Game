using UnityEngine;
using TMPro;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Canvas canvas;
    public Camera mainCamera;
    public GameObject[] produceFruitGameObjects;
    public GameObject[] inventoryFruitGameObjects;
    private readonly int[] startingQuantities = new int[11] { 4, 2, 4, 2, 2, 4, 4, 4, 1, 2, 4 };
    private int[] fruitQuantities;
    [SerializeField] private int[] fruitWeights = new int[11];
    [SerializeField] private int[] fruitValues = new int[11];
    public TMP_Text[] fruitQuantityTexts;
    private int totalWeight = 0;
    private int totalValue = 0;
    private readonly int maxWeight = 1040;
    private Vector2[] originalPositions;
    // New array to hold SpriteRenderers
    private SpriteRenderer[] _fruitRenderers;
    // Tooltip GameObject
    public GameObject tooltipGameObject;
    // Tooltip Text component
    private TMP_Text tooltipText;

    private void Start()
    {
        fruitQuantities = (int[])startingQuantities.Clone();
        originalPositions = new Vector2[produceFruitGameObjects.Length];
        _fruitRenderers = new SpriteRenderer[produceFruitGameObjects.Length];

        for (int i = 0; i < produceFruitGameObjects.Length; i++)
        {
            originalPositions[i] = produceFruitGameObjects[i].transform.position;
            _fruitRenderers[i] = produceFruitGameObjects[i].GetComponent<SpriteRenderer>();
        }

        UpdateInventoryTexts();

        // Get TextMeshPro component
        tooltipText = tooltipGameObject.GetComponent<TMP_Text>();

        // Hide tooltip at the start
        tooltipGameObject.SetActive(false);
    }

    public void ShowTooltip(int fruitIndex)
    {
        string tooltip = $"Weight: {fruitWeights[fruitIndex]} g Value: {fruitValues[fruitIndex]} gold";
        tooltipText.text = tooltip;
        Vector3 mousePosition = Input.mousePosition;
        float yOffset = 100f;
        mousePosition.y += yOffset;
        tooltipGameObject.transform.position = mousePosition;
        tooltipGameObject.SetActive(true);
    }

    public void FruitInBasket()
    {
        HideTooltip();
    }

    public void HideTooltip()
    {
        tooltipGameObject.SetActive(false);
    }

    public void AddFruitToBasket(int fruitIndex)
{
    if (fruitQuantities[fruitIndex] > 0)
    {
        totalWeight += fruitWeights[fruitIndex];
        totalValue += fruitValues[fruitIndex];
        fruitQuantities[fruitIndex]--;
        
        if (fruitQuantities[fruitIndex] == 0)
        {
            GameObject emptyFruit = Instantiate(inventoryFruitGameObjects[fruitIndex], originalPositions[fruitIndex], Quaternion.identity);
            emptyFruit.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            emptyFruit.layer = LayerMask.NameToLayer("Default");
        }

        UpdateInventoryTexts();
    }
}

    public void DecreaseFruitQuantity(int fruitIndex)
    {
        fruitQuantities[fruitIndex]--;

        if (fruitQuantities[fruitIndex] == 0)
        {
            inventoryFruitGameObjects[fruitIndex].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        UpdateInventoryTexts();
    }

    private void UpdateInventoryTexts()
    {
        for (int i = 0; i < fruitQuantities.Length; i++)
        {
            fruitQuantityTexts[i].text = fruitQuantities[i].ToString();
          
        }
    }

    public void CheckBasket()
    {
        if (totalWeight > maxWeight)
        {
            Debug.Log("Basket is too heavy! Resetting...");
            ResetBasket();
        }
        else
        {
            int totalFruitQuantity = 0;
            int totalStartingQuantity = 0;

            for (int i = 0; i < fruitQuantities.Length; i++)
            {
                totalFruitQuantity += fruitQuantities[i];
                totalStartingQuantity += startingQuantities[i];
            }

            if (totalFruitQuantity > totalStartingQuantity - 5)
            {
                Debug.Log("Not enough fruits! Resetting...");
                ResetBasket();
            }
            else
            {
                Debug.Log("Basket is within weight limit and has enough fruits. Total value of fruits: " + totalValue);
            }
        }
    }

    public void RemoveFruitFromBasket(int fruitIndex)
    {
        totalWeight -= fruitWeights[fruitIndex];
        totalValue -= fruitValues[fruitIndex];
        fruitQuantities[fruitIndex]++;

        if (fruitQuantities[fruitIndex] > 0)
        {
            GameObject fullFruit = Instantiate(inventoryFruitGameObjects[fruitIndex], originalPositions[fruitIndex], Quaternion.identity);
            fullFruit.GetComponent<SpriteRenderer>().color = Color.white;
            fullFruit.layer = LayerMask.NameToLayer("Default");
        }

        UpdateInventoryTexts();
    }


    public int GetFruitQuantity(int fruitIndex)
    {
        return fruitQuantities[fruitIndex];
    }

    public void ResetBasket()
    {
        totalWeight = 0;
        totalValue = 0;
        fruitQuantities = (int[])startingQuantities.Clone();
        UpdateInventoryTexts();


        for (int i = 0; i < produceFruitGameObjects.Length; i++)
        {
            produceFruitGameObjects[i].transform.position = originalPositions[i];
            produceFruitGameObjects[i].GetComponent<SpriteRenderer>().color = Color.white;

            if (produceFruitGameObjects[i].GetComponent<FruitbasketDragAndDrop>() == null)
            {
                produceFruitGameObjects[i].AddComponent<FruitbasketDragAndDrop>();
            }
            else
            {
                // Reset the isInBasket and isInInventory variables in the FruitbasketDragAndDrop component
                FruitbasketDragAndDrop fruitbasketDragAndDrop = produceFruitGameObjects[i].GetComponent<FruitbasketDragAndDrop>();
                fruitbasketDragAndDrop.isInBasket = false;
                fruitbasketDragAndDrop.isInInventory = true;
            }

            // Make the fruit's Rigidbody kinematic
            Rigidbody2D fruitRigidbody = produceFruitGameObjects[i].GetComponent<Rigidbody2D>();
            if (fruitRigidbody != null)
            {
                fruitRigidbody.isKinematic = true;
            }
        }

        // Add any other game state reset logic here if needed
    }


}
