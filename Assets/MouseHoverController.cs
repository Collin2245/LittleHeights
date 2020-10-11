using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MouseHoverController : MonoBehaviour
{
    // Start is called before the first frame update
    public Tile mouseHoverTile;
    public Tilemap mouseHoverTileMap; 
    Vector3 mousePos;
    Vector3Int prevPos;
    Vector3Int currPos;

    Vector3Int mousePosition;
    Vector3Int playerPosition;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition =  mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        playerPosition = mouseHoverTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(GameObject.FindGameObjectWithTag("Player").transform.position));
        Debug.Log("player: " + playerPosition.ToString());
        Debug.Log("mouse: " + mousePosition.ToString());
        if((Mathf.Abs(playerPosition.x) > Mathf.Abs(mousePosition.x) -200) || (Mathf.Abs(playerPosition.y) > Mathf.Abs(mousePosition.y) -200))
        {
            Debug.Log("Outside of target area");
        }
        else
        {
            SetMouseHoverTile();
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
