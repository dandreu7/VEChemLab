using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
    public Material defaultMaterial; // Display Screen Material
    public MeshRenderer displayRenderer; //Display Screen
    private List<string> itemsOnSensor = new List<string>();

    // Reactions Dictionary
    private Dictionary<List<string>, Material> reactionResults = new Dictionary<List<string>, Material>();

    void Start() {
        // How to use:
        // Add new reactions with the following format:
        // reactionResults.Add(new List<string> {CHEMICALS FOR REACTION HERE}, Resources.Load<Material>(PATH TO MATERIAL));
        // make sure that materials are under Assets/Resources/Materials/Reactions
        reactionResults.Add(new List<string> { "Baking Soda", "Vinegar" }, 
            Resources.Load<Material>("Materials/Reactions/Volcano Material"));
        reactionResults.Add(new List<string>{ "Mints", "Cola" },
            Resources.Load<Material>("Materials/Reactions/SodaBoom Material"));
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Pickups")) {
            string itemName = other.gameObject.name; // Gets name of item
            if (!itemsOnSensor.Contains(itemName) && itemsOnSensor.Count <= 5) { // Max of 5 items on the sensor
                itemsOnSensor.Add(itemName);
                CheckReaction();
                LogItemsOnSensor(); // Log when an item is added
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Pickups")) {
            string itemName = other.gameObject.name; // Gets name of item
            if (itemsOnSensor.Contains(itemName)) {
                itemsOnSensor.Remove(itemName);
                displayRenderer.material = defaultMaterial; // Reset the display when an item is removed
                LogItemsOnSensor(); // Log when an item is removed
            }
        }
    }

    private void CheckReaction() {
        foreach (var reaction in reactionResults) {
            // Check through dictionary to see if any of the chemicals on the sensor add up to a reaction in the dictionary
            if (new HashSet<string>(reaction.Key).SetEquals(itemsOnSensor)) {
                displayRenderer.material = reaction.Value;
                return;
            }
        }

        // If no reaction found, set to default material
        displayRenderer.material = defaultMaterial;
    }

    // Debug Log items on sensor
    private void LogItemsOnSensor() {
        string items = "Items on sensor: " + string.Join(", ", itemsOnSensor);
        Debug.Log(items);
    }
}
