using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 20;
    public List<ItemStack> items = new List<ItemStack>();

    public bool AddItem(ItemData item, int amount)
    {
        // Stack nếu có
        if (item.stackable)
        {
            foreach (var stack in items)
            {
                if (stack.item == item && stack.amount < item.maxStack)
                {
                    int addAmount = Mathf.Min(amount, item.maxStack - stack.amount);
                    stack.amount += addAmount;
                    amount -= addAmount;
                    if (amount <= 0) return true;
                }
            }
        }

        // Thêm slot mới
        while (amount > 0 && items.Count < maxSlots)
        {
            int addAmount = Mathf.Min(amount, item.maxStack);
            items.Add(new ItemStack(item, addAmount));
            amount -= addAmount;
        }

        return amount <= 0;
    }

    public bool HasItem(ItemData item, int amount)
    {
        int count = 0;
        foreach (var stack in items)
        {
            if (stack.item == item)
                count += stack.amount;
        }
        return count >= amount;
    }

    public void RemoveItem(ItemData item, int amount)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].item == item)
            {
                int remove = Mathf.Min(amount, items[i].amount);
                items[i].amount -= remove;
                amount -= remove;

                if (items[i].amount <= 0)
                    items.RemoveAt(i);

                if (amount <= 0)
                    return;
            }
        }
    }
}

