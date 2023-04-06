using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public bool HasBeenGrabbed;
    public Rigidbody2D GrabbableRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        GrabbableRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
