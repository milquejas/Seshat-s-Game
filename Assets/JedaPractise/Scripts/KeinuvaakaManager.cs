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
