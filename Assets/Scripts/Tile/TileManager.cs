using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    // for biokmes, make a bigger overarching perlin noise, have as input to chunk creation
    public Tilemap baseMap;
    public Tilemap extraMapNonCollide;
    public Tilemap extraMapCollide;
    private Tiles tiles;
    public int startMult;
    public int chunkSize;
    public float scale;
    public float seed;
    public Dictionary<Vector3Int, TileInfo> tileInfo;
    public Dictionary<Vector2Int, bool> drawnChunks;
    private bool useDictionaryOnAllTilesFlag;
    private bool playerPlaced;
    private GameObject player;
    void Start()
    {
        playerPlaced = false;
        useDictionaryOnAllTilesFlag = false;
        tiles = this.GetComponent<Tiles>();
        tileInfo = new Dictionary<Vector3Int, TileInfo>();
        drawnChunks = new Dictionary<Vector2Int, bool>();
        startMult = 500;
        seed = Random.Range(1f, 100000f);
        chunkSize = 35;
        scale = 0.6f;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(chunkSize * startMult + 0.5f * chunkSize, chunkSize * startMult + 0.5f * chunkSize, 0);
        DrawChunk(chunkSize, scale, seed,new Vector2Int(chunkSize*startMult, chunkSize*startMult));
        DrawChunksAroundPlayer();
        Debug.Log("seed: " + seed);
    }
    // Update is called once per frame
    void Update()
    {
        DrawChunksAroundPlayer();
        if (Input.GetMouseButtonDown(0))
        {
            GetTileInfoAtPoint();
        }
    }

    public TileInfo GetTileInfoAtPoint()
    {
        Vector3Int point = baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float perlin = perlinAtPoint(point);
        if (!tileInfo.ContainsKey(point))
        {

            if (perlin < 0.36f)
            {
                TileInfo ti = new TileInfo
                {
                    isWater = true
                };
                tileInfo.Add(point, ti);
            }
            else if (perlin > 0.36f && perlin <= 0.4f)
            {
                TileInfo ti = new TileInfo
                {
                    isWalkableWater = true
                };
                tileInfo.Add(point, ti);
            }
            else if (perlin > 0.4f && perlin <= 0.8f)
            {
                TileInfo ti = new TileInfo
                {
                    isGrass = true
                };
                tileInfo.Add(point, ti);
            }



        }
        Debug.Log("Is water?" + tileInfo[point].isWater);
        return tileInfo[point];
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

    public float perlinAtPoint(Vector3Int point)
    {
        float xF = (((float)point.x + seed) / (float)chunkSize * scale);
        float yF = ((float)point.y / (float)chunkSize * scale);
        return Mathf.PerlinNoise(xF, yF);
    }

    private void PlaceTileWithPerlin(float perlin, Vector3Int point)
    {
        if (perlin <= 0.36f)
        {
            GenerateDeepWaterTile(point);
        }
        else if (perlin > 0.36f && perlin <= 0.4f)
        {
            baseMap.SetTile(point, tiles.waterRuleTile);
            if(!tileInfo.ContainsKey(point) && useDictionaryOnAllTilesFlag)
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
            if (!tileInfo.ContainsKey(point) && useDictionaryOnAllTilesFlag)
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
            if (!tileInfo.ContainsKey(point) && useDictionaryOnAllTilesFlag)
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
            int randomNum = Random.Range(0, 200);
            if( randomNum % 2 == 1)
            {
                baseMap.SetTile(point, tiles.grass1Tile);
            }else
            {
                baseMap.SetTile(point, tiles.grass2Tile);
            }
            if (randomNum == 0 || randomNum == 1|| randomNum == 2)
            {
                //tI.isTreeOn = true;
                extraMapCollide.SetTile(point, tiles.orangeTreeSmallTile);
                TileInfo tI = new TileInfo();
                tI.isGrass = true;
                tI.isTreeOn = true;
                tileInfo.Add(point, tI);
        }
            else if (randomNum == 3)
            {
                //tI.isTreeOn = true;
                extraMapCollide.SetTile(point, tiles.deadTreeSmallTile);
                TileInfo tI = new TileInfo();
                tI.isGrass = true;
                tI.isWater = false;
                tI.isTreeOn = true;
                tileInfo.Add(point, tI);
        }
            else if (randomNum == 4)
            {
                //tI.isTreeOn = true;
                extraMapNonCollide.SetTile(point, tiles.purpleFlowerTile);
            }
            else if (randomNum == 5 ||randomNum == 6)
            {
                //tI.isTreeOn = true;
                extraMapCollide.SetTile(point, tiles.tealSmallEvergreenTile);
                TileInfo tI = new TileInfo();
                tI.isGrass = true;
                tI.isWater = false;
                tI.isTreeOn = true;
                tileInfo.Add(point, tI);
        }
            else if (randomNum == 7)
            {
                //tI.isTreeOn = true;
                extraMapNonCollide.SetTile(point, tiles.tealSmallBushTile);
            }
            else if (randomNum == 8)
            {
                //tI.isTreeOn = true;
                extraMapNonCollide.SetTile(point, tiles.grassWithRocksTile);
            }
            else if (randomNum >=15 && playerPlaced == false)
            {
                player.transform.position = point;
                playerPlaced = true;
        }
    }
    private void GenerateDeepWaterTile(Vector3Int point)
    {
        Color color = new Color(0.1f, 0.7f, 0.8f);
        baseMap.SetTile(point, tiles.waterRuleTile);
        extraMapCollide.SetTile(point, tiles.animatedWaterTile);
        baseMap.SetColor(point, color);
        extraMapCollide.SetColor(point, color);
        int randomNum = Random.Range(0, 100);
        if (randomNum == 0)
        {
            //add ti info here
            extraMapCollide.SetTile(point, tiles.rockOnWaterGray1Tile);
            extraMapCollide.SetColor(point, color);
        }
        else if (randomNum == 1 || randomNum == 2)
        {
            //add ti info here
            extraMapCollide.SetTile(point, tiles.lilyPadOnWaterTile);
            
        }
    }
}
