using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private Collider2D soundTriggerPoint;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            source.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        source.Stop();
    }
}
