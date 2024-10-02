using System.Collections.Generic;
using UnityEngine;

public class SensorPlane : MonoBehaviour {
    public Material defaultMaterial;
    public MeshRenderer displayRenderer;
    private List<string> itemsOnSensor = new List<string>();

    // Dictionary to store possible reactions
    private Dictionary<List<string>, Material> reactionResults = new Dictionary<List<string>, Material>();

    void Start() {
        // Add possible item combinations and their corresponding result materials
        reactionResults.Add(new List<string> { "Baking Soda", "Vinegar" }, Resources.Load<Material>("Materials/Reactions/Volcano Material"));
        // Add other reactions here
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Pickups")) {
            string itemName = other.gameObject.name;
            if (!itemsOnSensor.Contains(itemName) && itemsOnSensor.Count < 5) {
                itemsOnSensor.Add(itemName);
                CheckReaction();
                LogItemsOnSensor(); // Log items when an item is added
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Pickups")) {
            string itemName = other.gameObject.name;
            if (itemsOnSensor.Contains(itemName)) {
                itemsOnSensor.Remove(itemName);
                displayRenderer.material = defaultMaterial; // Reset the display when items are removed
                LogItemsOnSensor(); // Log items when an item is removed
            }
        }
    }

    private void CheckReaction() {
        foreach (var reaction in reactionResults) {
            if (new HashSet<string>(reaction.Key).SetEquals(itemsOnSensor)) {
                displayRenderer.material = reaction.Value;
                return;
            }
        }

        // Reset to default if no reaction found
        displayRenderer.material = defaultMaterial;
    }

    // Method to log the items currently on the sensor
    private void LogItemsOnSensor() {
        string items = "Items on sensor: " + string.Join(", ", itemsOnSensor);
        Debug.Log(items);
    }
}
