using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 movement;
    private Animator anim;
    private Rigidbody2D rb;

    private List<GameObject> cubes;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        cubes = new List<GameObject>();

        for (int i = 0; i <= 10; i++)
        {
            GameObject cube = GameObject.FindGameObjectWithTag("Object" + i);
            cubes.Add(cube);
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            //anim.SetFloat("horizontal", movement.x);
            //anim.SetFloat("vertical", movement.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Drop any cubes the player is carrying
            foreach (Transform child in transform)
            {
                child.parent = null;
                cubes.Add(child.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (cubes.Contains(other.gameObject))
        {
            other.gameObject.transform.parent = transform;
            cubes.Remove(other.gameObject);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (transform.childCount > 0 && transform.GetChild(0).gameObject == other.gameObject)
        {
            other.gameObject.transform.parent = null;
            cubes.Add(other.gameObject);
        }
    }
}
