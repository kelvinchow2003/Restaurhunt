using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Singleton pattern so we can access this from anywhere
    public static InventoryManager Instance;

    // This dictionary maps the Item Data to a Quantity count
    public Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep inventory when switching scenes!
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(ItemData item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        }
        else
        {
            inventory.Add(item, amount);
        }

        Debug.Log($"Added {amount} {item.itemName}. Total: {inventory[item]}");
    }

    public bool RemoveItem(ItemData item, int amount)
    {
        if (inventory.ContainsKey(item) && inventory[item] >= amount)
        {
            inventory[item] -= amount;
            
            // Optional: Remove the key entirely if count hits 0
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
            return true; // Success
        }
        return false; // Not enough items
    }
}