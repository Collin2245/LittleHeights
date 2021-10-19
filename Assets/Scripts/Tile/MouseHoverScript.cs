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
    bool isMainUIOn;
    Canvas mainUI;
    Vector3 mousePos;
    Vector3Int prevPos;
    Vector3Int currPos;

    Vector3Int mousePosition;
    Vector3Int playerPosition;

    void Awake()
    {
        mainUI = GameObject.Find("MainUI").GetComponent<Canvas>();
        mouseHoverTile.color = Color.white;
    }

    void Update()
    {
        isMainUIOn =  mainUI.isActiveAndEnabled;
        mousePosition =  mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        playerPosition = mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(GameObject.FindGameObjectWithTag("Player").transform.position));
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().activeArea &&  !isMainUIOn)
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
