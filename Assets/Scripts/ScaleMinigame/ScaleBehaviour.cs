using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * https://www.youtube.com/watch?v=criEPZC6z_Y
 * Finding a good reference to animate weighing scale like this was pain
 * 
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
 * 
 * Speed of item added to cup shakes scale?
 * 
 * (rightcup - leftcup)/ leftcup
 * Coroutine moving towards target rotation. Speed changes based on relative difference of weights. 
*/

public class ScaleBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D leftCupRBody;
    [SerializeField] private Rigidbody2D rightCupRBody;
    [SerializeField] private Rigidbody2D rotatingBeamRBody;

    [Header("Cup weights for testing")]
    [SerializeField] private float leftCupMass;
    [SerializeField] private float rightCupMass;

    [Header("Equal weight balance speed up")]
    [SerializeField] private float equalTorqueAmount;
    [SerializeField] private float equalTorqueTreshold;

    [SerializeField] private List<ItemSO> leftCupItems = new List<ItemSO>();
    [SerializeField] private List<ItemSO> rightCupItems = new List<ItemSO>();

    public void AddItemToScale(ScaleCup side, ItemSO addedItem)
    {
        if (side == ScaleCup.left)
        {
            leftCupItems.Add(addedItem);
        }

        if (side == ScaleCup.right)
        {
            rightCupItems.Add(addedItem);
        }

        UpdateMassForTesting();
    }

    public void RemoveItemFromScale(ScaleCup side, ItemSO addedItem)
    {
        if (side == ScaleCup.left)
        {
            leftCupItems.Remove(addedItem);
        }

        if (side == ScaleCup.right)
        {
            rightCupItems.Remove(addedItem);
        }

        // reset mass for empty cups, in case of bugs
        if (leftCupItems.Count == 0)
            leftCupRBody.mass = 1;

        if (rightCupItems.Count == 0)
            rightCupRBody.mass = 1;

        UpdateMassForTesting();
    }

    private void UpdateMassForTesting()
    {
        leftCupMass = leftCupRBody.mass;
        rightCupMass = rightCupRBody.mass;
    }

    // Speed up scale balancing when equal weights
    private void FixedUpdate()
    {
        if (leftCupRBody.mass == rightCupRBody.mass)
        {
            if (rotatingBeamRBody.rotation < -equalTorqueTreshold)
            {
                rotatingBeamRBody.AddTorque(equalTorqueAmount * rightCupRBody.mass);
            }
            if (rotatingBeamRBody.rotation > equalTorqueTreshold)
            {
                rotatingBeamRBody.AddTorque(-equalTorqueAmount * rightCupRBody.mass);
            }
        }
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
