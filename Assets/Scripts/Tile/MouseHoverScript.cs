using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MouseHoverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Tile mouseHoverTile;
    public Tilemap mouseHoverTileMap; 
    public bool isActiveArea;
    bool isInventoryOn;
    Vector3 mousePos;
    Vector3Int prevPos;
    Vector3Int currPos;

    Vector3Int mousePosition;
    Vector3Int playerPosition;

    void Update()
    {
        isInventoryOn =  GameObject.Find("InventoryCanvas").GetComponent<Canvas>().isActiveAndEnabled;
        mousePosition =  mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        playerPosition = mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(GameObject.FindGameObjectWithTag("Player").transform.position));
        if( !isInventoryOn)
        {
            SetAndDestroyTile();
            isActiveArea = true;
           
        }
        else
        {
            isActiveArea=false;
            mouseHoverTileMap.SetTile(currPos,null);
           
        }
    }


    void SetMouseHoverTile()
    {
        mouseHoverTileMap.SetTile(prevPos,null);
        mouseHoverTileMap.SetTile(currPos,mouseHoverTile);
        prevPos = currPos;
    }

    void SetAndDestroyTile()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currPos =  mouseHoverTileMap.WorldToCell(mousePos);
        if(prevPos != currPos)
        {
            SetMouseHoverTile();
        }
    }
}
