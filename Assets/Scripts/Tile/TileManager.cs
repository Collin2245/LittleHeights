using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    // for biokmes, make a bigger overarching perlin noise, have as input to chunk creation
    public Tilemap baseMap;
    public Tilemap extraMapNonCollide;
    public Tiles tiles;
    public int startMult;
    public int chunkSize;
    public float scale;
    public float seed;
    public Dictionary<Vector3Int, TileInfo> tileInfo;
    public Dictionary<Vector2Int, bool> drawnChunks;
    void Start()
    {
        tiles = this.GetComponent<Tiles>();
        tileInfo = new Dictionary<Vector3Int, TileInfo>();
        drawnChunks = new Dictionary<Vector2Int, bool>();
        startMult = 500;
        seed = Random.Range(1f, 100000f);
        chunkSize = 20;
        scale = 0.5f;
        DrawChunk(chunkSize, scale, seed,new Vector2Int(chunkSize*startMult, chunkSize*startMult));
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(chunkSize * startMult +0.5f*chunkSize, chunkSize * startMult + 0.5f * chunkSize, 0);
        Debug.Log("seed: " + seed);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //i += chunkSize;

            //seed = Random.Range(1f, 100000f);
            DrawChunksAroundPlayer();
            //Debug.Log("seed: " + seed);
            Debug.Log("chunk: " + new Vector2Int(chunkSize * startMult, chunkSize * startMult));
            Debug.Log("player Chunk:" + GetChunkAccordingToPlayerPos());
        }
    }

    void DrawChunk(int chunkSize, float scale, float seed, Vector2Int chunk)
    {
        if(!drawnChunks.ContainsKey(chunk) || !drawnChunks[chunk])
        {
            drawnChunks.Add(chunk, true);
            for (int y = chunk.y; y < chunkSize + chunk.y; y++)
            {
                for (int x = chunk.x; x < chunkSize + chunk.x; x++)
                {
                    Vector3Int point = new Vector3Int(x, y, 0);
                    float xF = (((float)x + seed) / (float)chunkSize * scale);
                    float yF = ((float)y / (float)chunkSize * scale);
                    float perlin = Mathf.PerlinNoise(xF, yF);
                    //Debug.Log(perlin);
                    PlaceTileWithPerlin(perlin, point);
                }
            }
        }
    }

    private void PlaceTileWithPerlin(float perlin, Vector3Int point)
    {
        if (perlin <= 0.3f)
        {
            GenerateDeepWaterTile(point);
        }
        else if (perlin > 0.3f && perlin <= 0.4f)
        {
            baseMap.SetTile(point, tiles.waterRuleTile);
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
            GenerateGrassTile(point);
        }
        else if (perlin > 0.8f && perlin <= 0.9f)
        {
            baseMap.SetTile(point, tiles.dirtTile);
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
            baseMap.SetTile(point, tiles.peakTile);
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

    Vector2Int GetChunkAccordingToPlayerPos()
    {
        Vector3Int playerWorldPos = baseMap.WorldToCell(GameObject.FindGameObjectWithTag("Player").transform.position);
        return new Vector2Int((playerWorldPos.x / chunkSize) *chunkSize, (playerWorldPos.y / chunkSize)*chunkSize);
    }

    void DrawChunksAroundPlayer()
    {
        Vector2Int currChunk = GetChunkAccordingToPlayerPos();
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x - chunkSize, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x + chunkSize, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x - chunkSize, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x, currChunk.y + chunkSize));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x + chunkSize, currChunk.y + chunkSize));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x + chunkSize, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new Vector2Int(currChunk.x - chunkSize, currChunk.y + chunkSize));
    }

    private void GenerateGrassTile(Vector3Int point)
    {
        baseMap.SetTile(point, tiles.grassTile);
        if (!tileInfo.ContainsKey(point))
        {
            TileInfo tI = new TileInfo
            {
                isGrass = true,
                isWater = false
            };
            int randomNum = Random.Range(0, 100);
            if (randomNum == 0 || randomNum == 1|| randomNum == 2)
            {
                tI.isTreeOn = true;
                extraMapNonCollide.SetTile(point, tiles.orangeTreeSmallTile);
            }
            else if (randomNum == 3)
            {
                tI.isTreeOn = true;
                extraMapNonCollide.SetTile(point, tiles.deadTreeSmallTile);
            }
            tileInfo.Add(point, tI);
        }
    }

    private void GenerateDeepWaterTile(Vector3Int point)
    {
        baseMap.SetTile(point, tiles.waterRuleTile);
        if (!tileInfo.ContainsKey(point))
        {
            TileInfo tI = new TileInfo();
            tI.isGrass = false;
            tI.isWater = true;
            int randomNum = Random.Range(0, 100);
            if (randomNum == 0)
            {
                //add ti info here
                extraMapNonCollide.SetTile(point, tiles.rockOnWaterGray1Tile);
            }
            else if (randomNum == 1 || randomNum == 2)
            {
                //add ti info here
                extraMapNonCollide.SetTile(point, tiles.whiteLilyPadOnWaterTile);
            }
            tileInfo.Add(point, tI);
        }
    }
}
