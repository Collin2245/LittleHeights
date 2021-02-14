using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap baseMap;
    public Tile tile;
    public int width;
    public int height;
    
    void Start()
    {
        width = 16;
        height = 9;
        drawMap(width, height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            drawMap(width, height);
        }
    }

    void drawMap(int width, int height)
    {
        for(int x = 0; x<width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.Log("x: " + x + " y: " + y);
                baseMap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}
