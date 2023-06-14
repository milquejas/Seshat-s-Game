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
    private readonly int maxWeight = 1000;
    private Vector2[] originalPositions;
    public GameObject tooltipGameObject;
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

        tooltipText = tooltipGameObject.GetComponent<TMP_Text>();
        tooltipGameObject.SetActive(false);
    }

    public void ShowTooltip(int fruitIndex)
    {
        string tooltip = $"Paino: {fruitWeights[fruitIndex]} g Arvo: {fruitValues[fruitIndex]} kultaa";
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
            fruitQuantities[fruitIndex]--;
            UpdateInventoryTexts();

            if (fruitQuantities[fruitIndex] == 0)
            {
                UpdateInventoryTexts();
            }

            if (totalWeight <= maxWeight) // Tarkista, että hedelmä mahtuu korin maksimipainoon
            {
                totalValue += fruitValues[fruitIndex]; // Päivitä kokonaisarvo vain, jos hedelmä lisätään koriin
            }

            int basketValue = CalculateBasketValue();
            Debug.Log("Basket value: " + basketValue);
        }
    }


    private int CalculateBasketValue()
    {
        int basketValue = 0;
        for (int i = 0; i < fruitQuantities.Length; i++)
        {
            int fruitValue = fruitValues[i] * (startingQuantities[i] - fruitQuantities[i]);
            basketValue += fruitValue;
        }
        return basketValue;
    }

    public void UpdateInventoryTexts()
    {
        for (int i = 0; i < fruitQuantities.Length; i++)
        {
            fruitQuantityTexts[i].text = fruitQuantities[i].ToString();
            Color fruitColor = fruitQuantities[i] <= 0 ? new Color(0.5f, 0.5f, 0.5f, 1) : Color.white;
            fruitGameObjects[i].GetComponent<SpriteRenderer>().color = fruitColor;

            if (fruitQuantities[i] == 0)
            {
                TMP_Text fruitText = fruitGameObjects[i].GetComponentInChildren<TMP_Text>();
                fruitText?.SetText("0"); // Päivitä tekstikomponentin arvo "0":ksi

                if (fruitText != null)
                {
                    fruitText.text = "0"; // Aseta hedelmän teksti "0" arvoksi
                }
            }
        }
    }


    public void CheckBasket()
    {
        if (totalWeight > maxWeight)
        {
            Debug.Log("Kori on liian painava! Nollataan...");
            ResetBasket();
        }
        else if (fruitQuantities.Sum() > startingQuantities.Sum() - 5)
        {
            Debug.Log("Ei tarpeeksi hedelmiä! Nollataan...");
            ResetBasket();
        }
        else
        {
            Debug.Log("Kori on sopivan painoinen ja siinä on tarpeeksi hedelmiä. Hedelmien kokonaisarvo: " + totalValue);
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
