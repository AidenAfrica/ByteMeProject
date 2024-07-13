using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Rigidbody2D rb; 
    private Vector2 movement;

    public GameObject information;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        information.SetActive(false);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("working");
            information.SetActive(true);
        }
    }

    public void Deactivate()
    {
        information.SetActive(false);
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}