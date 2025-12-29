using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public Inventory inventory;

    public bool Craft(CraftingRecipe recipe)
    {
        
        foreach (var ing in recipe.ingredients)
        {
            if (!inventory.HasItem(ing.item, ing.amount))
                return false;
        }

       
        foreach (var ing in recipe.ingredients)
        {
            inventory.RemoveItem(ing.item, ing.amount);
        }

        
        inventory.AddItem(recipe.resultItem, recipe.resultAmount);
        return true;
    }
}

