using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public float friction = 0.9f;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    private void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        rb.velocity = rb.velocity * friction;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
