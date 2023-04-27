using System.Linq;
using UnityEngine;

public class KeinuvaakaManager : MonoBehaviour
{
    public GameObject vasenkuppi, oikeakuppi;
    public Animator vaakaAnimator;

    private float painoEro;

    // Schedule a repeated check of the balance state
    void Start()
    {
        InvokeRepeating("TarkistaTasapaino", 0, 1.0f);
    }

    // Check if the balance is in equilibrium and log the result
    void TarkistaTasapaino()
    {
        float vasenkuppiPaino = LaskePaino(vasenkuppi);
        float oikeakuppiPaino = LaskePaino(oikeakuppi);
        painoEro = Mathf.Abs(vasenkuppiPaino - oikeakuppiPaino);

        vaakaAnimator.SetFloat("kallistus", vasenkuppiPaino - oikeakuppiPaino);
        vaakaAnimator.SetBool("palautaTasapainoon", painoEro < 10);

        if (painoEro < 10)
        {
            Debug.Log("Vaaka on tasapainossa!");
        }
        else
        {
            string painavampiKuppi = vasenkuppiPaino > oikeakuppiPaino ? "Vasen kuppi" : "Oikea kuppi";
            Debug.Log($"Vaaka on epätasapainossa. {painavampiKuppi} on painavampi.");
        }

        PrintItemTypesOnScale();
    }

    // Calculate the total weight of a specific item type on the scale.
    public float CalculateSpecificWeight(string itemType)
    {
        float totalWeight = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(vasenkuppi.transform.position, vasenkuppi.GetComponent<BoxCollider2D>().size, 0);
        colliders = colliders.Concat(Physics2D.OverlapBoxAll(oikeakuppi.transform.position, oikeakuppi.GetComponent<BoxCollider2D>().size, 0)).ToArray();

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
    public void PrintItemTypesOnScale()
    {
        Collider2D[] leftCupColliders = Physics2D.OverlapBoxAll(vasenkuppi.transform.position, vasenkuppi.GetComponent<BoxCollider2D>().size, 0);
        Collider2D[] rightCupColliders = Physics2D.OverlapBoxAll(oikeakuppi.transform.position, oikeakuppi.GetComponent<BoxCollider2D>().size, 0);

        foreach (Collider2D col in leftCupColliders)
        {
            WeightedObject weightedObject = col.GetComponent<WeightedObject>();
            if (weightedObject != null)
            {
                Debug.Log($"Item Type: {weightedObject.ItemType}, Weight: {weightedObject.Weight}, Cup: Left Cup");
            }
        }

        foreach (Collider2D col in rightCupColliders)
        {
            WeightedObject weightedObject = col.GetComponent<WeightedObject>();
            if (weightedObject != null)
            {
                Debug.Log($"Item Type: {weightedObject.ItemType}, Weight: {weightedObject.Weight}, Cup: Right Cup");
            }
        }
    }


    // Calculate the weight of objects in the specified cup
    float LaskePaino(GameObject kuppi)
    {
        float paino = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(kuppi.transform.position, kuppi.GetComponent<BoxCollider2D>().size, 0);

        // Add the weight of each object found in the cup
        foreach (Collider2D col in colliders)
        {
            WeightedObject weightedObject = col.GetComponent<WeightedObject>();
            if (weightedObject != null)
            {
                paino += weightedObject.Weight;
            }
        }
        return paino;
    }
}
