using UnityEngine;
using TMPro;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Canvas canvas;
    public Camera mainCamera;
    public GameObject[] fruitGameObjects;
    private readonly int[] startingQuantities = new int[11] { 4, 2, 4, 2, 2, 4, 4, 4, 1, 2, 4 };
    private int[] fruitQuantities;
    [SerializeField]
    private int[] fruitWeights = new int[11];

    [SerializeField]
    private int[] fruitValues = new int[11];
    public TMP_Text[] fruitQuantityTexts;
    private int totalWeight = 0;
    private int totalValue = 0;
    private readonly int maxWeight = 1040;
    private Vector2[] originalPositions;



    // Tooltip GameObject
    public GameObject tooltipGameObject;

    // Tooltip Text component
    private TMP_Text tooltipText;

    private void Start()
    {
        fruitQuantities = (int[])startingQuantities.Clone();
        originalPositions = new Vector2[fruitGameObjects.Length];
        for (int i = 0; i < fruitGameObjects.Length; i++)
        {
            originalPositions[i] = fruitGameObjects[i].transform.position;
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
            UpdateInventoryTexts();
        }
    }

    private void UpdateInventoryTexts()
    {
        for (int i = 0; i < fruitQuantities.Length; i++)
        {
            fruitQuantityTexts[i].text = fruitQuantities[i].ToString();
            Color fruitColor = fruitQuantities[i] == 0 ? new Color(0.5f, 0.5f, 0.5f, 1) : Color.white;
            fruitGameObjects[i].GetComponent<SpriteRenderer>().color = fruitColor;
        }
    }

    public void CheckBasket()
    {
        if (totalWeight > maxWeight)
        {
            Debug.Log("Basket is too heavy! Resetting...");
            ResetBasket();
        }
        else if (fruitQuantities.Sum() > startingQuantities.Sum() - 5)
        {
            Debug.Log("Not enough fruits! Resetting...");
            ResetBasket();
        }
        else
        {
            Debug.Log("Basket is within weight limit and has enough fruits. Total value of fruits: " + totalValue);
        }
    }


    public void ResetBasket()
    {
        totalWeight = 0;
        totalValue = 0;
        for (int i = 0; i < startingQuantities.Length; i++)
        {
            fruitQuantities[i] = startingQuantities[i];
        }
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
