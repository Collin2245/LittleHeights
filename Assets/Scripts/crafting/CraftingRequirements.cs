using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRequirements : MonoBehaviour
{
    // Start is called before the first frame update
    //public Dictionary<string, ItemRequirements[]> recipeRequirements;

    public static Dictionary<string, ItemRequirements[]>  GetRequirements()
    {
        return new Dictionary<string, ItemRequirements[]>()
        {
            { "craftingTable", new ItemRequirements[]{ ItemRequirement("acorn",2), ItemRequirement("wood",10)}},
            { "woodenAxe", new ItemRequirements[]{ ItemRequirement("wood",5)}}
        };
    }

    static ItemRequirements ItemRequirement(string item, int amountToUse)
    {
        return new ItemRequirements { id = item, amount = amountToUse };
    }

    public static Dictionary<string, List<string>> categoyItems = new Dictionary<string, List<string>>()
    {
        { "tools", new List<string> {"woodenAxe"} }
    };


}

public class ItemRequirements
{
    public string id;
    public int amount;
}