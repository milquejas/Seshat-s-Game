using System.Collections.Generic;
using UnityEngine;

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
 * Limit which side of the scale can take items.
 * Tutorial limits actions player can take. 
 * 
 * When something is added/removed
 * Update weights
 * Update animation 
 * Animation speed based on amount of weight difference added/removed
 * Shake before rotation starts?
*/ 

public class ScaleBehaviour : MonoBehaviour
{
    private float currentRotation;
    private int leftCombinedWeight;
    private int rightCombinedWeight;

    private Transform leftCup;
    private Transform rightCup;

    [SerializeField] private Transform rotatingBeam;
    [SerializeField] private Rigidbody2D rotatingBeamRBody;
    [SerializeField] private float maxRotationAngle = 20;
    [SerializeField] private List<ItemSO> leftCupItems = new List<ItemSO>();
    [SerializeField] private List<ItemSO> rightCupItems = new List<ItemSO>();

    public void AddItemToScale(ScaleCup side, ItemSO addedItem)
    {
        if (side == ScaleCup.left)
        {
            leftCupItems.Add(addedItem);
            leftCombinedWeight += addedItem.ItemWeight;
        }

        else
        {
            rightCupItems.Add(addedItem);
            rightCombinedWeight += addedItem.ItemWeight;
        }

        UpdateWeights();
    }

    public void RemoveItemFromScale(ScaleCup side, ItemSO addedItem)
    {

    }

    private void UpdateWeights()
    {
        CalculateRotation();
    }

    private void CalculateRotation()
    {
        rotatingBeamRBody.rotation += 0.3f;
        //rotatingBeam.Rotate(0, 0, 0.3f);
        //leftCombinedWeight - rightCombinedWeight
    }
}
public class Quest
{
    public string description;
    public ItemType itemType;
    public float targetWeight;
}
public enum ScaleCup
{
    left,
    right,
}
