using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class SimpleTopdownController : MonoBehaviour
{
    [Header("Camera")]
    public Camera playerCamera; // Camera that views player
    public Vector3 relativePosition;
    public float rotateSpeed;

    [Header("Movement")]
    public Joystick movementInput;
    public Joystick aimInput;
    public float speed;

    Rigidbody2D rb;
    Vector2 movementData;
    Vector2 movementValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementData = new Vector2(movementInput.Direction.x, movementInput.Direction.y);
        movementData.Normalize();

        movementValue = new Vector2(movementData.x * speed, movementData.y * speed);

        Vector2 aimData = new Vector2(aimInput.Direction.x, aimInput.Direction.y);
        aimData.Normalize();

        if(aimInput.Direction != Vector2.zero)
        {
            rb.MoveRotation(Vector2.SignedAngle(Vector2.up, aimData));
        }
        
    }

    private void FixedUpdate()
    {
        if (movementData.x != 0 || movementData.y != 0)
        {
            rb.MovePosition(rb.position + movementValue * Time.deltaTime);
        }

    }
}
