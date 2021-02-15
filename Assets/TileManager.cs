using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap baseMap;
    public Tile tile;
    public Tile tile2;
    public Tile tile3;
    public Tile tile4;
    public Tile tile5;
    public int width;
    public int height;
    public float scale;
    public float seed;
    Vector2 offset;
    void Start()
    {
        offset = new Vector2(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
        seed = Random.Range(0.01f, 0.99f);
        width = 100;
        height = 100;
        scale = 4500;
        drawMap(width, height, scale);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            seed = Random.Range(0.01f, 0.99f);
            drawMap(width, height, scale);
        }
    }

    void drawMap(int width, int height, float scale)
    {
        for(int y = 0; y<width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                Vector3Int point = new Vector3Int(x, y, 0);
                float xF = ((float)x/(float)scale + offset.x);
                float yF = ((float)y/(float)scale + offset.y);
                float perlin = Mathf.PerlinNoise(xF, yF);
                Debug.Log(perlin);
                if(perlin <= 0.2f)
                {
                    baseMap.SetTile(point, tile);
                }
                else if(perlin > 0.2f && perlin <= 0.4f)
                {
                    baseMap.SetTile(point, tile2);
                }
                else if (perlin > 0.4f && perlin <= 0.6f)
                {
                    baseMap.SetTile(point, tile3);
                }
                else if (perlin > 0.8f && perlin <= 0.9f)
                {
                    baseMap.SetTile(point, tile4);
                }
                else if (perlin > 0.9f && perlin <= 1.0f)
                {
                    baseMap.SetTile(point, tile5);
                }
            }
        }
    }
}
