using UnityEngine;

public class KeinuvaakaManager : MonoBehaviour
{
    public GameObject kuppi1, kuppi2;
    public int porkkanaPaino = 250;
    public int omenaPaino = 150;
    public int banaaniPaino = 50;

    private float painoEro;

    // Schedule a repeated check of the balance state
    void Start()
    {
        InvokeRepeating("TarkistaTasapaino", 0, 1.0f);
    }

    // Check if the balance is in equilibrium and log the result
    void TarkistaTasapaino()
    {
        float kuppi1Paino = LaskePaino(kuppi1);
        float kuppi2Paino = LaskePaino(kuppi2);
        painoEro = Mathf.Abs(kuppi1Paino - kuppi2Paino);

        if (painoEro < 10)
        {
            Debug.Log("Vaaka on tasapainossa!");
        }
        else
        {
            Debug.Log($"Vaaka ei ole tasapainossa. Painoero on {painoEro}g.");
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
            if (col.CompareTag("Porkkana"))
            {
                paino += porkkanaPaino;
            }
            else if (col.CompareTag("Omena"))
            {
                paino += omenaPaino;
            }
            else if (col.CompareTag("Banaani"))
            {
                paino += banaaniPaino;
            }
        }
        return paino;
    }
}
