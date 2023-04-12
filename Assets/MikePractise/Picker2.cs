using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker2 : MonoBehaviour
{
    public float speed = 5f;
    public List<GameObject> cubes = new List<GameObject>();

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (cubes.Contains(other.gameObject))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.transform.SetParent(transform);
            cubes.Remove(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent == transform)
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.transform.SetParent(null);
            cubes.Add(other.gameObject);
        }
    }
}
