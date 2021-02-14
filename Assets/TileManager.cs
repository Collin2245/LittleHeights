using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap baseMap;
    public Tile tile;
    public Tile tile2;
    public int width;
    public int height;
    
    void Start()
    {
        width = 100;
        height = 100;
        drawMap(width, height);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            drawMap(width, height);
        }
        if (Input.GetMouseButton(0))
        {
            baseMap.SetTileFlags(baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), TileFlags.None);
            baseMap.SetColor(baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Color.blue);
        }
    }

    void drawMap(int width, int height)
    {
        for(int y = 0; y<width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                Vector3Int point = new Vector3Int(x, y, 0);
                float xF = (float)x/(float)width;
                float yF = (float)y/(float)height;
                float perlin = Mathf.PerlinNoise(xF, yF);
                Debug.Log(perlin);
                if(perlin <= 0.5f)
                {
                    baseMap.SetTile(point, tile);
                }else
                {
                    baseMap.SetTile(point, tile2);
                }
            }
        }
    }
}
