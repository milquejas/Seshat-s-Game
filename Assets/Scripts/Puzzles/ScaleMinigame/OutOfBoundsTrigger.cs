using UnityEngine;

/*
 * When weighted item hits this. Place it back in pool with animation 
*/ 

public class OutOfBoundsTrigger : MonoBehaviour
{
    [SerializeField] private ScaleMinigamePooler pooler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponentInParent<DraggableWeightedItem>() != null)
        {
            DraggableWeightedItem item = collision.GetComponentInParent<DraggableWeightedItem>();

            StartCoroutine(pooler.ReturnItemToPool(item, 0.5f));
        }
    }
}
