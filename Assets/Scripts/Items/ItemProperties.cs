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
        {"tempHoe",5 },
        {"itemName",999 },
        {"wood",99},
        {"acorn",99},
        {"woodenAxe",10},
        {"laser",55 }
    };


    public static Dictionary<string, string> itemPlaced = new Dictionary<string, string>()
    {
        { "acorn", "sapplingPrefab" },
        { "wood", "woodPrefab"},
        { "woodenAxe", ""}
    };

    public static Dictionary<string, bool> itemIsPlaceable = new Dictionary<string, bool>()
    {
        { "acorn", true },
        { "wood", false},
        { "woodenAxe", false}
    };


    //public Dictionary<string, Tilemap> itemsTilemap = new Dictionary<string, Tilemap>()
    //{
    //    {"acorn", placeableSoil}
    //};
}



