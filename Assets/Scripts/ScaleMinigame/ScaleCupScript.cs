using UnityEngine;

/*
 * Check when produce collides. 
 * Stick produce to surface
 * If produce edge is over certain absolute from centre, dont stick
 * Check on exit if item hit edge
 * 
*/ 

public class ScaleCupScript : MonoBehaviour
{
    private Collider2D cupCollider;
    private ScaleBehaviour scaleBehaviour;
    private Rigidbody2D cupRBody;
    [SerializeField] private float massReductionAmount;
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
        float distance = collision.transform.position.x - transform.position.x;
        DraggableWeightedItem item = collision.gameObject.GetComponent<DraggableWeightedItem>();

        if (Mathf.Abs(distance) <= cupStickynessLimitWidth && item.transform.position.y >= transform.position.y - itemUnderCupHeight)
        {
            Physics2D.IgnoreCollision(cupCollider, collision.collider, true);
            item.RBody.isKinematic = true;

            item.ItemIsInThisCup = this;

            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.angularVelocity = 0f;

            cupRBody.mass += item.Item.ItemWeight / massReductionAmount;
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
        cupRBody.mass -= item.Item.ItemWeight / massReductionAmount;
        item.ItemIsInThisCup = null;

        scaleBehaviour.RemoveItemFromScale(side, item.Item);

        item.transform.SetParent(item.originalParent, true);

        Physics2D.IgnoreCollision(cupCollider, item.GetComponentInChildren<Collider2D>(), false);
    }
}