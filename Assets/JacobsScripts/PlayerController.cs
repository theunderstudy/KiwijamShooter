using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setup stuff")]
    public Transform gunLocator;
    public Rigidbody2D rb;

    [Header("Movement stuff")]
    public float walkSpeed = 100;
    public float runSpeed = 300;

    [Header("Debug stuff")]
    public Vector2 input;
    private Vector2 position;

    void Update()
    {
        position = rb.position;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input = Vector2.ClampMagnitude(input, 1);

        rb.MovePosition(position + walkSpeed * input * Time.deltaTime);
    }
}
