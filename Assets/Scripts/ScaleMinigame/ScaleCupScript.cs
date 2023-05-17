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
    [SerializeField] private float cupStickynessLimitWidth;
    [SerializeField] private float itemUnderCupHeight;
    [SerializeField] private ScaleCup side;

    private void Start()
    {
        scaleBehaviour = GetComponentInParent<ScaleBehaviour>();
        cupCollider = GetComponent<Collider2D>();
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

            scaleBehaviour.AddItemToScale(side, item.Item);

            item.transform.SetParent(transform, true);
        }

        else
        {
            collision.rigidbody.velocity = new Vector2(distance * 3, 1);
        }
    }

    public void RemoveFromCup(DraggableWeightedItem item)
    {
        Debug.Log("item exited cup");
        scaleBehaviour.RemoveItemFromScale(side, item.Item);

        item.transform.SetParent(item.originalParent, true);

        Physics2D.IgnoreCollision(cupCollider, item.GetComponentInChildren<Collider2D>(), false);
    }

    // if thing chills on cup somehow, but has not been entered/added, this checks and adds
    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DraggableWeightedItem item))
        {
            if (item.EnteredScaleCupProperly) return;

            float distance = collision.transform.position.x - transform.position.x;
            if (Mathf.Abs(distance) <= cupStickynessLimitWidth && item.transform.position.y >= transform.position.y - itemUnderCupHeight)
            {
                collision.rigidbody.velocity = Vector2.zero;
                collision.rigidbody.angularVelocity = 0f;

                item.EnteredScaleCupProperly = true;
                scaleBehaviour.AddItemToScale(side, item.Item);

                item.transform.SetParent(transform, true);

                Debug.Log("Why did this OnCollisionStay2D run?");
            }
        }
    }*/
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        DraggableWeightedItem item = collision.gameObject.GetComponent<DraggableWeightedItem>();
        if (item.EnteredScaleCupProperly)
        {
            Debug.Log("item exited cup");
            item.EnteredScaleCupProperly = false;
            scaleBehaviour.RemoveItemFromScale(side, item.Item);

            item.transform.SetParent(item.originalParent, true);
        }
    }*/
}