using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public ItemData itemToGive; // Drag your ScriptableObject here in Inspector
    public int quantity = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding is the Player
        if (other.CompareTag("Player"))
        {
            // Add to the global inventory
            InventoryManager.Instance.AddItem(itemToGive, quantity);
            
            // Destroy the physical object from the world
            Destroy(gameObject);
        }
    }
}