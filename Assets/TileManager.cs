using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    // for biokmes, make a bigger overarching perlin noise, have as input to chunk creation
    public Tilemap baseMap;
    public Tile tile;
    public Tile tile2;
    public Tile tile3;
    public Tile tile4;
    public Tile tile5;
    public RuleTile ruleTile;
    public int startMult;
    public int chunkSize;
    public float scale;
    public float seed;
    int i;
    public Dictionary<Vector3Int, TileInfo> tileInfo;
    void Start()
    {
        tileInfo = new Dictionary<Vector3Int, TileInfo>();
        startMult = 500;
        seed = Random.Range(1f, 100000f);
        chunkSize = 20;
        scale = 0.5f;
        DrawChunk(chunkSize, scale, seed,new Vector2Int(chunkSize*startMult, chunkSize*startMult));
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(chunkSize * startMult +0.5f*chunkSize, chunkSize * startMult + 0.5f * chunkSize, 0);
        Debug.Log("seed: " + seed);
        Debug.Log(tileInfo.Count);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            i += chunkSize;

            //seed = Random.Range(1f, 100000f);
            DrawChunk(chunkSize, scale, seed, new Vector2Int((chunkSize * startMult)- chunkSize, (chunkSize * startMult) - chunkSize));
            DrawChunk(chunkSize, scale, seed, new Vector2Int((chunkSize * startMult) + chunkSize, chunkSize * startMult));
            DrawChunk(chunkSize, scale, seed, new Vector2Int((chunkSize * startMult) - chunkSize, chunkSize * startMult));
            DrawChunk(chunkSize, scale, seed, new Vector2Int(chunkSize * startMult, (chunkSize * startMult) + chunkSize));
            DrawChunk(chunkSize, scale, seed, new Vector2Int(chunkSize * startMult, (chunkSize * startMult) - chunkSize));
            DrawChunk(chunkSize, scale, seed, new Vector2Int((chunkSize * startMult) + chunkSize, (chunkSize * startMult) + chunkSize));
            DrawChunk(chunkSize, scale, seed, new Vector2Int((chunkSize * startMult) + chunkSize, (chunkSize * startMult) - chunkSize));
            DrawChunk(chunkSize, scale, seed, new Vector2Int((chunkSize * startMult) - chunkSize, (chunkSize * startMult) + chunkSize));
            Debug.Log("seed: " + seed);
        }
    }

    void DrawChunk(int chunkSize, float scale, float seed, Vector2Int chunk)
    {
        for(int y = chunk.y; y< chunkSize + chunk.y; y++)
        {
            for (int x = chunk.x; x < chunkSize + chunk.x; x++)
            {
                Vector3Int point = new Vector3Int(x, y, 0);
                float xF = (((float)x+seed)/(float)chunkSize * scale);
                float yF = ((float)y/(float)chunkSize * scale);
                float perlin = Mathf.PerlinNoise(xF, yF);
                //Debug.Log(perlin);
                PlaceTileWithPerlin(perlin, point);
            }
        }
        Debug.Log("Chunk:" + chunk);
    }

    private void PlaceTileWithPerlin(float perlin, Vector3Int point)
    {
        if (perlin <= 0.35f)
        {
            baseMap.SetTile(point, ruleTile);
            if(!tileInfo.ContainsKey(point))
            {
                TileInfo tI = new TileInfo();
                tI.isGrass = false;
                tI.isWater = true;
                tileInfo.Add(point, tI);
            }
        }
        else if (perlin > 0.35f && perlin <= 0.4f)
        {
            baseMap.SetTile(point, ruleTile);
            if(!tileInfo.ContainsKey(point))
            {
                TileInfo tI = new TileInfo();
                tI.isGrass = false;
                tI.isWater = true;
                tileInfo.Add(point, tI);
            }
        }
        else if (perlin > 0.4f && perlin <= 0.8f)
        {
            baseMap.SetTile(point, tile3);
            if (!tileInfo.ContainsKey(point))
            {
                TileInfo tI = new TileInfo();
                tI.isGrass = false;
                tI.isWater = true;
                tileInfo.Add(point, tI);
            }
        }
        else if (perlin > 0.8f && perlin <= 0.9f)
        {
            baseMap.SetTile(point, tile4);
            if (!tileInfo.ContainsKey(point))
            {
                TileInfo tI = new TileInfo();
                tI.isGrass = false;
                tI.isWater = true;
                tileInfo.Add(point, tI);
            }
        }
        else if (perlin > 0.9f)
        {
            baseMap.SetTile(point, tile5);
            if (!tileInfo.ContainsKey(point))
            {
                TileInfo tI = new TileInfo();
                tI.isGrass = false;
                tI.isWater = true;
                tileInfo.Add(point, tI);
            }
        }
        else
        {
            Debug.LogError(perlin);
        }
    }
}
