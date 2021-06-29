using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRequirements : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, ItemRequirements[]> recipeRequirements;

    private void Start()
    {
        Dictionary<string, ItemRequirements[]> recipeRequirements = new Dictionary<string, ItemRequirements[]>()
        {
            { "craftingTable", new ItemRequirements[]{ ItemRequirement("acorn",2), ItemRequirement("wood",10)}},
            { "woodenAxe", new ItemRequirements[]{ ItemRequirement("wood",5)}}
        };


    }


    ItemRequirements ItemRequirement(string item, int amountToUse)
    {
        return new ItemRequirements { id = item, amount = amountToUse };
    }

}

public class ItemRequirements
{
    public string id;
    public int amount;
}