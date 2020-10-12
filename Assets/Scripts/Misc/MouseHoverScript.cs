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
    Vector3 mousePos;
    Vector3Int prevPos;
    Vector3Int currPos;

    Vector3Int mousePosition;
    Vector3Int playerPosition;
    int playerPosOffsetX;
    int playerPosOffsetY;
    void Update()
    {
        mousePosition =  mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        playerPosition = mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(GameObject.FindGameObjectWithTag("Player").transform.position));
        playerPosOffsetY = mousePosition.y - playerPosition.y - 5;
        playerPosOffsetX = mousePosition.x - 9 -playerPosition.x;
        if(playerPosOffsetX <4 && playerPosOffsetX > -4 && playerPosOffsetY <4 && playerPosOffsetY> -4)
        {
            SetAndDestroyTile();
            isActiveArea = true;
            Debug.Log("Inside of target area");
        }
        else
        {
            isActiveArea=false;
            mouseHoverTileMap.SetTile(currPos,null);
            Debug.Log("Outside of target area");
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
