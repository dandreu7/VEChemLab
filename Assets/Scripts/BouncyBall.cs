using UnityEngine;

public class BouncyBall : MonoBehaviour {
    //public float bounceForce = 5f; // Force applied when bouncing
    private Rigidbody rb;
    public float friction = 0.9f;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        rb.velocity = rb.velocity * friction;
    }

    /* void OnCollisionEnter(Collision collision) {
        // Check if the ball is colliding with the ground or a surface, then bounce
        if (collision.relativeVelocity.magnitude > 1) {
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }*/
}
