using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
            tryToHoverTile(currentItem.id);
            if(Input.GetMouseButtonDown(1))
            {
                Debug.Log("tried to place " + currentItem.id);
                // Debug.Log("tried to place on " + ItemProperties.itemsTilemap[currentItem.id]);
                Debug.Log("tried to make prefab " + ItemProperties.itemPlaced[currentItem.id]);
            }
        }
        else
        {
            placeableItemTileMap.SetTile(prevPos,null);
            placeableItemTileMap.SetTile(currPos,null);
        }
    }

    bool tryToHoverTile(string itemId)
    {
        
        if(isActive)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = Resources.Load<Sprite>("Items/" + itemId); 
            // placeableItemTileMap.SetTile(mousePosition,tile);
            SetAndDestroyTile(tile);
            return true;
        }
        else
        {
            placeableItemTileMap.SetTile(prevPos,null);
            placeableItemTileMap.SetTile(currPos,null);
            currPos =  placeableItemTileMap.WorldToCell(mousePos);
        }
        return false;
    }

     void SetMouseHoverTile(Tile tile)
    {
        placeableItemTileMap.SetTile(prevPos,null);
        placeableItemTileMap.SetTile(currPos,tile);
        prevPos = currPos;
    }

    void SetAndDestroyTile(Tile tile)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currPos =  placeableItemTileMap.WorldToCell(mousePos);
        if(prevPos != currPos)
        {
            SetMouseHoverTile(tile);
        }
    }
}
