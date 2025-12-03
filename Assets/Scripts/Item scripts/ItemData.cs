using UnityEngine;

// This allows us to right-click -> Create -> New Item
[CreateAssetMenu(fileName = "New Item", menuName = "Restaurhunt/Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Game Data")]
    public ItemType itemType;
    public int sellPrice; // How much it sells for in the restaurant
}

public enum ItemType
{
    Ingredient, // Found in dungeon
    Dish        // Cooked in kitchen
}