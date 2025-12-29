using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<RecipeIngredient> ingredients;
    public ItemData resultItem;
    public int resultAmount = 1;
}
[System.Serializable]
public class RecipeIngredient
{
    public ItemData item;
    public int amount;
}
