using UnityEngine;
using static AntiqueScaleExtensions;

/*
 * Collision with DraggableWeightedItem adds item to ScaleBehaviour list,
 * Disables collisions, sets to kinematic and changes items parent
 * Remove from cup is called from interacting with DraggableWeightedItem after items ItemIsInThisCup is not null
*/

public class ScaleCupScript : MonoBehaviour
{
    private Collider2D cupCollider;
    private ScaleBehaviour scaleBehaviour;
    private Rigidbody2D cupRBody;
    [SerializeField] private float cupStickynessLimitWidth;
    [SerializeField] private float itemUnderCupHeight;
    [SerializeField] private ScaleCup side;
    [SerializeField] private Transform ParentForItem;

    private void Start()
    {
        scaleBehaviour = GetComponentInParent<ScaleBehaviour>();
        cupCollider = GetComponent<Collider2D>();
        cupRBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DraggableWeightedItem>().ItemIsInThisCup is not null) return;

        float distance = collision.transform.position.x - transform.position.x;
        DraggableWeightedItem item = collision.gameObject.GetComponent<DraggableWeightedItem>();

        if (Mathf.Abs(distance) <= cupStickynessLimitWidth && item.transform.position.y >= transform.position.y - itemUnderCupHeight)
        {
            Physics2D.IgnoreCollision(cupCollider, collision.collider, true);
            item.RBody.isKinematic = true;

            item.ItemIsInThisCup = this;

            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.angularVelocity = 0f;

            cupRBody.mass += ConvertRealWeightToUnityMass(item.Item.ItemWeight);
            scaleBehaviour.AddItemToScale(side, item.Item);

            item.transform.SetParent(ParentForItem, true);
        }

        else
        {
            collision.rigidbody.velocity = new Vector2(distance * 3, 1);
        }
    }

    public void RemoveFromCup(DraggableWeightedItem item)
    {
        cupRBody.mass -= ConvertRealWeightToUnityMass(item.Item.ItemWeight);
        item.ItemIsInThisCup = null;

        scaleBehaviour.RemoveItemFromScale(side, item.Item);

        item.transform.SetParent(item.originalParent, true);

        Physics2D.IgnoreCollision(cupCollider, item.GetComponentInChildren<Collider2D>(), false);
    }
}