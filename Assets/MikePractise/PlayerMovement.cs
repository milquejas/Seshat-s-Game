using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.9f;

    private Vector2 movement;
    private Animator anim;
    private Rigidbody2D rb;

    //public List<GameObject> cubes;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //cubes = new List<GameObject>();

        //for (int i = 0; i <= 10; i++)
        //{
        //    GameObject cube = GameObject.FindGameObjectWithTag("Object" + i);
        //    cubes.Add(cube);
        //}
        //
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= runSpeedMultiplier;
        }

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
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("Collision");
    //    if (cubes.Contains(other.gameObject))
    //    {
    //        Destroy(other.gameObject);
    //        cubes.Remove(other.gameObject);
    //        Debug.Log("Tuho");

    //    }
    //}
}
/* 
 * Tämä sripti toimii niin että kun pelaaja koskettaa objektia objekti tuhoutuu.
 *  Samassa skriptissä listataan objekteja. Olisi hyvä että Objektien listaus tapahtuisi omassa skriptissään. 
 */
