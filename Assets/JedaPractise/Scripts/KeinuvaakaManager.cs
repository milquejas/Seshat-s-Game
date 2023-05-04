/*
 * When stuff stays in a cup for 2sec, sticky them and add them to the weight pool? 
 * Every time something is added, update weights and animation? 
 * Every time something is removed, update weights and animation? 
 * If scale is even && one side has only weights -> enable accept trade button? 
 * Accepting trade gives you what trade ui says. 
 * Inventory update sends event to quest manager. 
 * QuestManager compares items you got to quest target. 
 * Start from beginning/reset inventory if wrong. 
 * 
 * Tip button to show what is inside a cup and their weights?
 * 
*/
/*
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WeightedObject;

public class KeinuvaakaManager : MonoBehaviour
{
    public GameObject leftCup, rightCup;
    public Animator scaleAnimator;
    private float weightDifference;
    public TaskManager taskManager;



    void Start()
    {
        // Check the balance state every 1.0 seconds
        InvokeRepeating("CheckBalance", 0, 1.0f);
    }

    // Check if the balance is in equilibrium and log the result
    void CheckBalance()
    {
        // Calculate the weights of the left and right cups
        float leftCupWeight = CalculateWeight(leftCup);
        float rightCupWeight = CalculateWeight(rightCup);

        // Calculate the absolute difference between the weights of the cups
        weightDifference = Mathf.Abs(leftCupWeight - rightCupWeight);

        // Update the animator parameters to reflect the weight difference
        scaleAnimator.SetFloat("kallistus", leftCupWeight - rightCupWeight);
        scaleAnimator.SetBool("palautaTasapainoon", weightDifference < 10);

        // Log the balance state based on the weight difference
        if (weightDifference < 10)
        {
            Debug.Log("The scale is balanced!");
            if (taskManager != null)
            {
                taskManager.CheckTaskCompletion();
            }
        }
        else
        {
            string heavierCup = leftCupWeight > rightCupWeight ? "Left cup" : "Right cup";
            Debug.Log($"The scale is unbalanced. {heavierCup} is heavier.");
        }

        // Log the items and their weights on the scale
        PrintItemTypesOnScale();
    }

    // Calculate the total weight of a specific item type on the scale.
    public float CalculateSpecificWeight(ItemType itemType)
    {
        float totalWeight = 0;
        // Get all colliders in both cups
        Collider2D[] colliders = Physics2D.OverlapBoxAll(leftCup.transform.position, leftCup.GetComponent<BoxCollider2D>().size, 0);
        colliders = colliders.Concat(Physics2D.OverlapBoxAll(rightCup.transform.position, rightCup.GetComponent<BoxCollider2D>().size, 0)).ToArray();

        // Add up the weights of the items of the specified type
        foreach (Collider2D col in colliders)
        {
            WeightedObject weightedObject = col.GetComponent<WeightedObject>();
            if (weightedObject != null && weightedObject.ItemType == itemType)
            {
                totalWeight += weightedObject.Weight;
            }
        }
        return totalWeight;
    }

    // Log the item types and their counts and weights in each cup
    public void PrintItemTypesOnScale()
    {
        PrintCupItems(leftCup, "Left Cup");
        PrintCupItems(rightCup, "Right Cup");
    }

    // Log the item types and their counts and weights in the specified cup
    private void PrintCupItems(GameObject cup, string cupName)
    {
        // Get all colliders in the specified cup
        Collider2D[] cupColliders = Physics2D.OverlapBoxAll(cup.transform.position, cup.GetComponent<BoxCollider2D>().size, 0);
        Dictionary<ItemType, ItemData> itemDataDict = new Dictionary<ItemType, ItemData>();
        float totalCupWeight = 0;

        // Iterate through the colliders and group the items by type, counting and summing their weights
        foreach (Collider2D col in cupColliders)
        {
            WeightedObject weightedObject = col.GetComponent<WeightedObject>();
            if (weightedObject != null)
            {
                ItemType itemType = weightedObject.ItemType;
                if (itemDataDict.ContainsKey(itemType))
                {
                    itemDataDict[itemType].Count++;
                    itemDataDict[itemType].TotalWeight += weightedObject.Weight;
                }
                else
                {
                    itemDataDict[itemType] = new ItemData { Count = 1, TotalWeight = weightedObject.Weight };
                }
                totalCupWeight += weightedObject.Weight;
            }
        }    // Log the total weight of the specified cup
        Debug.Log($"Cup: {cupName}, Total Weight: {totalCupWeight}");

        // Log the item types, their counts, and their total weights in the specified cup
        foreach (var item in itemDataDict)
        {
            ItemType itemType = item.Key;
            int count = item.Value.Count;
            float totalWeight = item.Value.TotalWeight;
            Debug.Log($"Item Type: {itemType}, Count: {count}, Total Weight: {totalWeight}, Cup: {cupName}");
        }
    }

    // Helper class for storing item count and total weight
    private class ItemData
    {
        public int Count { get; set; }
        public float TotalWeight { get; set; }
    }

    // Calculate the weight of objects in the specified cup
    float CalculateWeight(GameObject cup)
    {
        float weight = 0;
        // Get all colliders in the specified cup
        Collider2D[] colliders = Physics2D.OverlapBoxAll(cup.transform.position, cup.GetComponent<BoxCollider2D>().size, 0);

        // Add the weight of each object found in the cup
        foreach (Collider2D col in colliders)
        {
            WeightedObject weightedObject = col.GetComponent<WeightedObject>();
            if (weightedObject != null)
            {
                weight += weightedObject.Weight;
            }
        }
        return weight;
    }
}
*/