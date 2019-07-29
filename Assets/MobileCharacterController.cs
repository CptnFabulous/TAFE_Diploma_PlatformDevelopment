using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCharacterController : MonoBehaviour
{
    public Joystick movementInput;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new Vector3(movementInput.input.x * speed, movementInput.input.y * speed, 0);

        transform.Translate(movementDirection * Time.deltaTime, Space.World);
    }
}
