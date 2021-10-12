using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingProperties : MonoBehaviour
{
    // Start is called before the first frame update
    //public Dictionary<string, ItemRequirements[]> recipeRequirements;

    public static Dictionary<string, ItemRequirements[]>  GetRequirements()
    {
        return new Dictionary<string, ItemRequirements[]>()
        {
            { "craftingTable", new ItemRequirements[]{ ItemRequirement("acorn",2), ItemRequirement("wood",10)}},
            { "woodenAxe", new ItemRequirements[]{ ItemRequirement("wood",5)}},
            { "sapling", new ItemRequirements[]{ ItemRequirement("acorn",1) , ItemRequirement("wood", 5) } },
            { "twine", new ItemRequirements[]{ ItemRequirement("leaves",3)} },
            { "stick", new ItemRequirements[]{ ItemRequirement("wood",1) } }
        };
    }

    public static Dictionary<string, bool> RecipeUnlocked = new Dictionary<string, bool>()
        {
            { "craftingTable", false },
            { "woodenAxe", false },
            { "sapling", false },
            { "twine", false },
            { "stick", false }
        };

    public static Dictionary<string, int> craftingItemAmount = new Dictionary<string, int>()
    {
        {"stick", 5 },
        {"twine", 2 }
    };

    public static Dictionary<string, List<string>> categoyItems = new Dictionary<string, List<string>>()
    {
        { "tools", new List<string> {"woodenAxe"} },
        {"farmingCategory", new List<string> {"sapling","twine","stick"} }
    };

    public static string[] categoryNames = new string[] { "farmingCategory", "toolsCategory" };

    static ItemRequirements ItemRequirement(string item, int amountToUse)
    {
        return new ItemRequirements { id = item, amount = amountToUse };
    }


}

public class ItemRequirements
{
    public string id;
    public int amount;
}