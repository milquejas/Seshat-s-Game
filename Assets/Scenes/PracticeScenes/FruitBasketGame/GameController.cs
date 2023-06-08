using System;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Reference to the fruit GameObjects in the inventory
    public GameObject[] fruitGameObjects;

    // Starting quantities of each fruit
    private readonly int[] startingQuantities = new int[11] { 4, 2, 4, 2, 2, 4, 4, 4, 1, 2, 4 };

    // Current quantities of each fruit
    private int[] fruitQuantities;

    // Weights of each fruit
    private readonly int[] fruitWeights = new int[11] { 1, 2, 1, 2, 2, 1, 1, 1, 3, 2, 1 };

    // Values of each fruit
    private readonly int[] fruitValues = new int[11] { 1, 4, 3, 5, 4, 1, 3, 1, 7, 6, 3 };

    // TMP_Text references for displaying the quantity of each fruit
    public TMP_Text[] fruitQuantityTexts;

    // Total weight of the fruits in the basket
    private int totalWeight = 0;

    // Total value of the fruits in the basket
    private int totalValue = 0;

    // Maximum allowed weight in the basket
    private readonly int maxWeight = 11;

    // Original positions of the fruit GameObjects
    private Vector2[] originalPositions;

    private void Start()
    {
        // Clone the starting quantities array to the current fruitQuantities array
        fruitQuantities = (int[])startingQuantities.Clone();

        // Save the original positions of the fruit GameObjects
        originalPositions = new Vector2[fruitGameObjects.Length];
        for (int i = 0; i < fruitGameObjects.Length; i++)
        {
            originalPositions[i] = fruitGameObjects[i].transform.position;
        }

        // Update the displayed fruit quantities
        UpdateInventoryTexts();
    }

    // Function to add a fruit to the basket
    public void AddFruitToBasket(int fruitIndex)
    {
        // Check if there is at least one of this fruit type left in the inventory
        if (fruitQuantities[fruitIndex] > 0)
        {
            // If so, add the weight and value of the fruit to the total weight and value
            totalWeight += fruitWeights[fruitIndex];
            totalValue += fruitValues[fruitIndex];

            // Decrease the quantity of this fruit in the inventory
            fruitQuantities[fruitIndex]--;

            // Update the displayed fruit quantities
            UpdateInventoryTexts();
        }
    }

    // Function to update the displayed fruit quantities
    private void UpdateInventoryTexts()
    {
        for (int i = 0; i < fruitQuantities.Length; i++)
        {
            // Update the quantity text for this fruit
            fruitQuantityTexts[i].text = fruitQuantities[i].ToString();

            // If there are no more of this fruit type left in the inventory, darken its sprite
            if (fruitQuantities[i] == 0)
            {
                fruitGameObjects[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            // If there are some of this fruit type left in the inventory, restore its sprite color to white
            else
            {
                fruitGameObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    // Function to check the contents of the basket
    public void CheckBasket()
    {
        // If the total weight of the fruits in the basket is more than the maximum allowed weight
        if (totalWeight > maxWeight)
        {
            // Log a message and reset the basket
            Debug.Log("Basket is too heavy! Resetting...");
            ResetBasket();
        }
        else
        {
            // If the total weight is within the limit, log a message indicating the total value of the fruits
            Debug.Log("Basket is within weight limit. Total value of fruits: " + totalValue);
        }
    }

    // Function to reset the basket
    public void ResetBasket()
    {
        // Reset the total weight and value
        totalWeight = 0;
        totalValue = 0;

        // Clone the starting quantities array to the current fruitQuantities array
        fruitQuantities = (int[])startingQuantities.Clone();

        // Reset the fruit GameObjects to their original positions
        for (int i = 0; i < fruitGameObjects.Length; i++)
        {
            fruitGameObjects[i].transform.position = originalPositions[i];
            fruitGameObjects[i].SetActive(true);
        }

        // Update the displayed fruit quantities
        UpdateInventoryTexts();
    }
}