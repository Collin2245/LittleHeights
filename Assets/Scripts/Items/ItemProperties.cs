using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemProperties : MonoBehaviour
{

    //public static Tilemap placeableSoil;

    void Start()
    {
        //set up tileMaps
        //placeableSoil = GameObject.FindGameObjectWithTag("PlaceableSoil").GetComponent<Tilemap>();
    }
    public static Dictionary<string, int> quantityForItem = new Dictionary<string, int>()
    {
        {"Blank", 1 },
        {"Square",2 },
        {"woodenSword",1 },
        {"woodenHoe",1 },
        {"itemName",999 },
        {"wood",99},
        {"acorn",99},
        {"woodenAxe",1},
        {"laser",55 },
        {"craftingBench",99 },
        {"sapling",99 },
        {"leaves", 999 },
        {"stick",99 },
        {"twine",99 }
    };


    public static Dictionary<string, string> itemPlaced = new Dictionary<string, string>()
    {
        { "acorn", "sapplingPrefab" },
        { "wood", "woodPrefab"},
        { "woodenAxe", ""},
        {"sapling","saplingPrefab"}
    };

    public static Dictionary<string, bool> itemIsPlaceable = new Dictionary<string, bool>()
    {
        { "acorn", true },
        { "wood", false},
        { "woodenAxe", false},
        { "craftingBench", true },
        { "sapling", true }
    };

    public static Dictionary<string, string> itemDescription = new Dictionary<string, string>()
    {
        {"sapling","A simple sapling, use this to grow a tree."},
        {"twine", "A simple means to make basic tools." },
        {"woodenAxe", "Use this to chop down trees much faster than with your hands" },
        {"woodenSword", "The most basic sword, you need this to attempt to do any damage to your enemies"},
        {"stick", "Temp description" },
        {"fertilizer", "Temp description" },
        {"flowerSeeds", "Temp description" },
        {"woodenHoe", "Temp description" },
    };


    //public Dictionary<string, Tilemap> itemsTilemap = new Dictionary<string, Tilemap>()
    //{
    //    {"acorn", placeableSoil}
    //};
}



