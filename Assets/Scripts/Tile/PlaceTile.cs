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
    Grid grid;
    PlayerInventory playerInventory;
    public CurrentItem hotBarItem;
    public Item currentItem;
    public Tilemap placeableItemTileMap;
    public Tile tileToPlace;
    GameObject ItemPrefabOnMouse;
    GameObject placedItem;
    Vector3Int mousePosition;
    Vector3Int prevPos;
    Vector3Int currPos;
    Vector3 mousePos;
    int placeableItemMask;
    void Start()
    {
        activeTileSelector = GameObject.FindGameObjectWithTag("TileManager");
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        grid = GameObject.FindObjectOfType<Grid>();
        currentItem = playerInventory.currentItem;
        hotBarItem = GameObject.Find("CurrentItemSelector").GetComponentInChildren<CurrentItem>();
        placeableItemMask = LayerMask.GetMask("PlaceableItem");
    }

    // Update is called once per frame
    void Update()
    {
        isActive = activeTileSelector.GetComponent<MouseHoverScript>().isActiveArea;
        currentItem = playerInventory.currentItem;
        mousePosition =  placeableItemTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(currentItem && isActive && ItemProperties.itemIsPlaceable[currentItem.id])
        {
            tryToHoverTile(currentItem.id);
            if(Input.GetMouseButtonDown(1))
            {
                Debug.Log("tried to place " + currentItem.id);
                // Debug.Log("tried to place on " + ItemProperties.itemsTilemap[currentItem.id]);
                Debug.Log("tried to make prefab " + ItemProperties.itemPlaced[currentItem.id]);
                try
                {
                    if(!checkIfItemThere())
                    {
                        placedItem = Instantiate(Resources.Load("PlaceableItem/" + currentItem.id), grid.GetCellCenterWorld(mousePosition), Quaternion.identity) as GameObject;
                        HotbarItemHolder hotbarItemHolder = hotBarItem.currentSlot;
                        Debug.Log("Position: " + grid.GetCellCenterWorld(mousePosition));
                        hotbarItemHolder.itemHolderOnInventory.GetComponentInChildren<Item>().subtractQuantity(1);
                    }
                    else
                    {
                        Debug.Log("Item already there");
                    }
                }
                catch
                {
                    Debug.Log("error placing item");
                }
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
    private bool checkIfItemThere()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, placeableItemMask);
        if (hit.collider != null && hit.transform.name == this.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}