using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker4 : MonoBehaviour
{
    public float speed = 5f;
    public List<GameObject> cubes = new List<GameObject>();
    public KeyCode collectKey = KeyCode.Space;
    public KeyCode dropKey = KeyCode.Return;

    private Rigidbody2D rb;
    private bool carryingCube = false;
    private GameObject carriedCube;
    private bool canCarry = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * speed;

        if (Input.GetKeyDown(collectKey) && canCarry)
        {
            if (!carryingCube && cubes.Count > 0)
            {
                carriedCube = cubes[0];
                carriedCube.transform.SetParent(transform);
                carriedCube.transform.localPosition = new Vector3(0, 0.5f, 0);
                cubes.RemoveAt(0);
                carryingCube = true;
            }
        }

        if (Input.GetKeyDown(dropKey))
        {
            if (carryingCube)
            {
                carriedCube.transform.SetParent(null);
                cubes.Add(carriedCube);
                carriedCube = null;
                carryingCube = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Object"))
        {
            canCarry = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            canCarry = false;
        }
    }
}
