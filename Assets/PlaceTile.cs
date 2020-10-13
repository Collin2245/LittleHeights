using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlaceTile : MonoBehaviour
{
    // Start is called before the first frame update
    bool isActive;
    GameObject activeTileSelector;
    PlayerInventory playerInventory;
    public Item currentItem;

    public Tilemap placeableItemTileMap;
    public Tile tileToPlace;
    GameObject ItemPrefabOnMouse;
    Vector3Int mousePosition;
    Vector3Int prevPos;
    Vector3Int currPos;
    Vector3 mousePos;
    void Start()
    {
        activeTileSelector = GameObject.FindGameObjectWithTag("TileManager");
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        currentItem = playerInventory.currentItem;
    }

    // Update is called once per frame
    void Update()
    {
        isActive = activeTileSelector.GetComponent<MouseHoverScript>().isActiveArea;
        currentItem = playerInventory.currentItem;
        mousePosition =  placeableItemTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(currentItem)
        {
            tryToPlaceTile(currentItem.id);
        }
        

    }

    bool tryToPlaceTile(string itemId)
    {
        
        if(isActive)
        {
            Tile tile = new Tile();
            tile.sprite = Resources.Load<Sprite>("Items/" + itemId); 
            placeableItemTileMap.SetTile(mousePosition,tile);
            return true;
        }
        return false;
    }

     void SetMouseHoverTile()
    {
        placeableItemTileMap.SetTile(prevPos,null);
        placeableItemTileMap.SetTile(currPos,mouseHoverTile);
        prevPos = currPos;
    }

    void SetAndDestroyTile()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currPos =  placeableItemTileMap.WorldToCell(mousePos);
        if(prevPos != currPos)
        {
            SetMouseHoverTile();
        }
    }
}
