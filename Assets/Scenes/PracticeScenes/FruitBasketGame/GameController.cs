using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Canvas canvas;
    public Camera mainCamera;
    public GameObject[] fruitGameObjects;
    private readonly int[] startingQuantities = new int[11] { 4, 2, 4, 2, 2, 4, 4, 4, 1, 2, 4 };
    private int[] fruitQuantities;
    private readonly int[] fruitWeights = new int[11] { 1, 2, 1, 2, 2, 1, 1, 1, 3, 2, 1 };
    private readonly int[] fruitValues = new int[11] { 1, 4, 3, 5, 4, 1, 3, 1, 7, 6, 3 };
    public TMP_Text[] fruitQuantityTexts;
    private int totalWeight = 0;
    private int totalValue = 0;
    private readonly int maxWeight = 11;
    private Vector2[] originalPositions;

    public GameObject tooltipGameObject; // Tooltip GameObject
    private TMP_Text tooltipText; // Tooltip Text component

    private void Start()
    {
        fruitQuantities = (int[])startingQuantities.Clone();
        originalPositions = new Vector2[fruitGameObjects.Length];
        for (int i = 0; i < fruitGameObjects.Length; i++)
        {
            originalPositions[i] = fruitGameObjects[i].transform.position;
        }
        UpdateInventoryTexts();

        tooltipText = tooltipGameObject.GetComponent<TMP_Text>(); // Get TextMeshPro component
        tooltipGameObject.SetActive(false); // Hide tooltip at the start
    }
    public void ShowTooltip(int fruitIndex)
    {
        string tooltip = $"Weight: {fruitWeights[fruitIndex]} Value: {fruitValues[fruitIndex]}";
        tooltipText.text = tooltip;

        // Calculate the position of the tooltip to match the mouse cursor
        Vector3 mousePosition = Input.mousePosition;

        // Adjust the position so the tooltip appears above the mouse
        float yOffset = 150f; // Change this value to adjust the vertical offset of the tooltip
        mousePosition.y += yOffset;

        // Set the position of the tooltip
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
            UpdateInventoryTexts();
        }
    }

    private void UpdateInventoryTexts()
    {
        for (int i = 0; i < fruitQuantities.Length; i++)
        {
            fruitQuantityTexts[i].text = fruitQuantities[i].ToString();
            if (fruitQuantities[i] == 0)
            {
                fruitGameObjects[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                fruitGameObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
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
            Debug.Log("Basket is within weight limit. Total value of fruits: " + totalValue);
        }
    }

    public void ResetBasket()
    {
        totalWeight = 0;
        totalValue = 0;
        fruitQuantities = (int[])startingQuantities.Clone();
        for (int i = 0; i < fruitGameObjects.Length; i++)
        {
            fruitGameObjects[i].transform.position = originalPositions[i];
            fruitGameObjects[i].SetActive(true);
        }
        UpdateInventoryTexts();
    }

    public int GetFruitQuantity(int fruitIndex)
    {
        return fruitQuantities[fruitIndex];
    }
}