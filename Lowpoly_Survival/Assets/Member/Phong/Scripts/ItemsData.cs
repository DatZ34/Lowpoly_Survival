using UnityEngine;

public enum ItemType
{
    Resource,
    Consumable,
    Equipment
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public bool stackable = true;
    public int maxStack = 99;
}
[System.Serializable]
public class ItemStack
{
    public ItemData item;
    public int amount;

    public ItemStack(ItemData item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
