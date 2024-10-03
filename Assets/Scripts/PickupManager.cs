using UnityEngine;
using UnityEngine.InputSystem;

public class PickupMananger : MonoBehaviour {
    public float pickupRange = 3f; 
    public float holdDistance = 0.5f; // Hold distance from the player
    public float holdHeight = -0.2f;  
    private GameObject heldObject = null;
    public Camera playerCamera; // Manually choose the used camera

    void Start() {
        //playerCamera = Camera.main;
        //Debug.Log("PickupSystem initialized. Using camera: " + playerCamera.name);
    }

    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("Left click detected.");
            if (heldObject == null)
            {
                TryPickup();
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame) {
            Debug.Log("Right click detected.");
            if (heldObject != null)
            {
                ReleaseObject();
            }
        }

        if (heldObject != null) {
            HoldObject();
        }
    }

    void TryPickup() {
        // Cast a ray from the center of the screen (invisible cursor)
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // Draw the ray in the Scene view for debugging purposes
        //Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red, 2f);

        Debug.Log("Attempting to pick up object. Raycast from cursor position.");
    
        if (Physics.Raycast(ray, out hit, pickupRange)) {
            Debug.Log("Raycast hit object: " + hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Pickups")) {
                Debug.Log("Object has 'Pickups' tag. Picking it up.");
                heldObject = hit.collider.gameObject;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null) {
                    // Disable physics while holding the object
                    rb.isKinematic = true; 
                    rb.useGravity = false;
                }
                else {
                    Debug.LogWarning("Picked object does not have a Rigidbody!");
                }
            }
            else {
                Debug.Log("Hit object is not tagged as 'Pickups'.");
            }
        }
        else {
            Debug.Log("Raycast did not hit any objects within pickup range.");
        }
    }


    void HoldObject() {
        if (heldObject != null) {
            Debug.Log("Holding object: " + heldObject.name);
            // Position the object to the right side of the player, close to avoid clipping
            Vector3 holdPos = playerCamera.transform.position +
                              playerCamera.transform.right * holdDistance +
                              playerCamera.transform.forward * holdHeight;

            // Adjust the position to avoid clipping through walls
            heldObject.transform.position = holdPos;
            heldObject.transform.rotation = playerCamera.transform.rotation;
        }
    }

    void ReleaseObject() {
        if (heldObject != null) {
            Debug.Log("Releasing object: " + heldObject.name);
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null) {
                // Enable physics while not holding the object
                rb.isKinematic = false;
                rb.useGravity = true;
                
                // Toss object
                Vector3 tossDirection = playerCamera.transform.forward + Vector3.up * 0.2f; // Add a slight upward component
                rb.AddForce(tossDirection * 5f, ForceMode.Impulse); // Apply a toss force
                Debug.Log("Tossing object with force in direction: " + tossDirection);
            }
            
            heldObject = null;
        }
    }
}
