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
    [SerializeField] private float torqueAmount;
    [SerializeField] private float equalTorqueTreshold;
    [SerializeField] private float torqueRotationRange;

    public List<ItemSO> leftCupItems = new List<ItemSO>();
    public List<ItemSO> rightCupItems = new List<ItemSO>();

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

    public bool ScaleIsBalanced()
    {
        if (leftCupMass == rightCupMass)
            return true;

        return false;
    }

    // Speed up scale movement with extra torque
    private void FixedUpdate()
    {
        if (leftCupRBody.mass == rightCupRBody.mass)
        {
            if (rotatingBeamRBody.rotation < -equalTorqueTreshold)
            {
                rotatingBeamRBody.AddTorque(torqueAmount * rightCupRBody.mass);
            }
            if (rotatingBeamRBody.rotation > equalTorqueTreshold)
            {
                rotatingBeamRBody.AddTorque(-torqueAmount * rightCupRBody.mass);
            }
        }

        if (leftCupRBody.mass > rightCupRBody.mass && rotatingBeamRBody.rotation > -torqueRotationRange) 
        {
            rotatingBeamRBody.AddTorque(torqueAmount * leftCupRBody.mass);
        }

        if (rightCupRBody.mass > leftCupRBody.mass && rotatingBeamRBody.rotation < torqueRotationRange)
        {
            rotatingBeamRBody.AddTorque(-torqueAmount * rightCupRBody.mass);
        }
    }

    public void LeaveScaleMinigame()
    {
        GameManager.GameManagerInstance.LoadScene("IsometricMain");
    }
}

public enum ScaleCup
{
    left,
    right,
}
