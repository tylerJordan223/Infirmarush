using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool canControl;
    private Vector2 inputVector;
    private Vector2 lastInputVector;
    public static int health;
    private float playerSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        canControl = true;
        health = 3;
        playerSpeed = 5f;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canControl)
        {
            //update input
            inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //update last input if actual input and not 0
            // CURRENTLY SET UP FOR 4 DIRECTIONAL, IF HORIZONTAL THEN USE THAT
            if ((inputVector.x != 0 || inputVector.y != 0) && (inputVector.x == 0 || inputVector.y == 0))
            {
                lastInputVector = inputVector;
            }
        }
    }

    private void FixedUpdate()
    {
        //normalize for the sake of diagonal not double speed
        inputVector.Normalize();

        //update on speed if alive, stop if else
        if (health > 0)
        {
            rb.velocity = inputVector * playerSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
