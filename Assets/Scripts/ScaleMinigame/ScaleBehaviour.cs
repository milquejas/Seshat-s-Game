using System.Collections.Generic;
using UnityEngine;

/*
 * https://www.youtube.com/watch?v=criEPZC6z_Y
 * Finding a good reference to animate weighing scale like this was pain
 * 
 * If scale is balanced, add torque to speed up balancing
 * Contains Lists of items in each cup for quests
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
