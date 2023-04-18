using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunnitseEsineet : MonoBehaviour

{
    public GameObject vasenVaakakuppi;
    public GameObject oikeaVaakakuppi;
    public float tavoitePaino;
    public float porkkanaPaino;
    public float omenaPaino;
    public float banaaniPaino;
    private float vasenPaino = 0f;
    private float oikeaPaino = 0f;

    public void LisaaVasempaanVaakakuppiin(GameObject esine)
    {
        if (esine.CompareTag("Porkkana"))
        {
            vasenPaino += porkkanaPaino;
        }
        else if (esine.CompareTag("Omena"))
        {
            vasenPaino += omenaPaino;
        }
        else if (esine.CompareTag("Banaani"))
        {
            vasenPaino += banaaniPaino;
        }
        esine.transform.position = vasenVaakakuppi.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
        esine.transform.parent = vasenVaakakuppi.transform;
        vasenVaakakuppi.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -vasenPaino * 10f));
        TarkistaTavoitePaino();
    }

    public void LisaaOikeaanVaakakuppiin(GameObject esine)
    {
        if (esine.CompareTag("Porkkana"))
        {
            oikeaPaino += porkkanaPaino;
        }
        else if (esine.CompareTag("Omena"))
        {
            oikeaPaino += omenaPaino;
        }
        else if (esine.CompareTag("Banaani"))
        {
            oikeaPaino += banaaniPaino;
        }
        esine.transform.position = oikeaVaakakuppi.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
        esine.transform.parent = oikeaVaakakuppi.transform;
        oikeaVaakakuppi.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -oikeaPaino * 10f));
        TarkistaTavoitePaino();
    }

    private void TarkistaTavoitePaino()
    {
        if (Mathf.Approximately(vasenPaino, oikeaPaino))
        {
            Debug.Log("Tavoitepaino saavutettu!");
        }
    }

    void OnMouseDown()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }

    void OnMouseUp()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}