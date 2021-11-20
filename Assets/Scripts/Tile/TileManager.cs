using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

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
    public Dictionary<MocVector2int, Dictionary<MocVector3int, TileInfo>> tileInfo;
    public Dictionary<MocVector2int, bool> drawnChunks;    
    public List<KeyValuePair<MocVector2int, List<KeyValuePair<MocVector3int, TileInfo>>>> tileInfoPersistent;
    private bool useDictionaryOnAllTilesFlag;
    private bool playerPlaced;
    private GameObject player;
    public TileManager Instance;

    void Start()
    {
        LoadWithPersistentData();
    }

    void LoadWithPersistentData()
    {
        playerPlaced = false;
        tiles = this.GetComponent<Tiles>();
        //converts because json serialization is stupid sometimes
        tileInfoPersistent = PersistentData.Instance.CurrentWorld.tileInfo;
        tileInfo = tileInfoPersistent.ToDictionary(x => x.Key, x => x.Value.ToDictionary(x => x.Key, x => x.Value));
        drawnChunks = new Dictionary<MocVector2int, bool>();
        startMult = 500;
        seed = PersistentData.Instance.CurrentWorld.seed;
        chunkSize = 35;
        scale = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(chunkSize * startMult + 0.5f * chunkSize, chunkSize * startMult + 0.5f * chunkSize, -10);
        DrawChunk(chunkSize, scale, seed, new Vector2Int(chunkSize * startMult, chunkSize * startMult));
        DrawChunksAroundPlayer();
        Debug.Log("seed: " + seed);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") !=0)
        {
            DrawChunksAroundPlayer();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdateInstance();
            SaveHelper.NewWorldSave(PersistentData.Instance.CurrentWorld);
        }
    }

    void UpdateInstance()
    {
        ConvertToListOfList(tileInfo);
        PersistentData.Instance.CurrentWorld.tileInfo = tileInfoPersistent;
    }

    void ConvertToListOfList(Dictionary<MocVector2int, Dictionary<MocVector3int, TileInfo>> tileInfo)
    {
        tileInfoPersistent.Clear();
        List<KeyValuePair<MocVector3int, TileInfo>> points = new List<KeyValuePair<MocVector3int, TileInfo>>();
        foreach (KeyValuePair<MocVector2int, Dictionary<MocVector3int,TileInfo >> chunk in tileInfo)
        {
            points.Clear();
            foreach(KeyValuePair<MocVector3int, TileInfo> point in chunk.Value)
            {
                points.Add(new KeyValuePair<MocVector3int, TileInfo>(point.Key, point.Value));
            }
            tileInfoPersistent.Add(new KeyValuePair<MocVector2int, List<KeyValuePair<MocVector3int,TileInfo>>>(chunk.Key, points));
        }
    }

    public TileInfo GetTileInfoAtPoint()
    {
        Vector3Int pointV3 = baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        MocVector3int point;
        point.x = pointV3.x;
        point.y = pointV3.y;
        point.z = pointV3.z;
        float perlin = perlinAtPoint(point);
        if (!tileInfo.ContainsKey(point))
        {

            if (perlin < 0.35f)
            {

            }
            else if (perlin > 0.35f && perlin <= 0.4f)
            {

            }
            else if (perlin > 0.4f && perlin <= 0.8f)
            {

            }
        }
        //Debug.Log("Is water?" + tileInfo[point].isWater);
        return tileInfo[point];
    }
    public TileInfo GetTileInfoAtPoint(MocVector3int point)
    {
        float perlin = perlinAtPoint(point);
        if (!tileInfo.ContainsKey(point))
        {

            if (perlin < 0.35f)
            {
                TileInfo ti = new TileInfo
                {
                    isWater = true
                };
                tileInfo.Add(point, ti);
            }
            else if (perlin > 0.35f && perlin <= 0.4f)
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
        return tileInfo[point];
    }

    void DrawChunk(int chunkSize, float scale, float seed, MocVector2int chunk)
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
                    if(tileInfo.ContainsKey(chunk))
                    {
                        LoadTileWithPerlin(perlin, point);
                    }
                    else
                    {
                        tileInfo.Add(chunk, new Dictionary<MocVector3int, TileInfo>());
                        PlaceTileWithPerlin(perlin,chunk,point);
                    }
                    
                }
            }
        }
    }

    public float perlinAtPoint(MocVector3int point)
    {
        float xF = (((float)point.x + seed) / (float)chunkSize * scale);
        float yF = ((float)point.y / (float)chunkSize * scale);
        return Mathf.PerlinNoise(xF, yF);
    }
    private void LoadTileWithPerlin(float perlin, Vector3Int point)
    {
        if (perlin <= 0.36f)
        {
            GenerateDeepWaterTile(point);
        }
        else if (perlin > 0.36f && perlin <= 0.4f)
        {
            baseMap.SetTile(point, tiles.waterRuleTile);
        }
        else if (perlin > 0.4f && perlin <= 0.8f)
        {
            GenerateGrassTile(point);
        }
        else if (perlin > 0.8f && perlin <= 0.9f)
        {
            baseMap.SetTile(point, tiles.dirtTile);
        }
        else if (perlin > 0.9f)
        {
            baseMap.SetTile(point, tiles.peakTile);
        }
        else
        {
            Debug.LogError(perlin);
        }
    }
    private void PlaceTileWithPerlin(float perlin,Vector2Int chunk, Vector3Int point)
    {
        if (perlin <= 0.36f)
        {
            GenerateDeepWaterTile(point);
        }
        else if (perlin > 0.36f && perlin <= 0.4f)
        {
            baseMap.SetTile(point, tiles.waterRuleTile);
        }
        else if (perlin > 0.4f && perlin <= 0.8f)
        {
            GenerateGrassTile(point);
        }
        else if (perlin > 0.8f && perlin <= 0.9f)
        {
            baseMap.SetTile(point, tiles.dirtTile);
        }
        else if (perlin > 0.9f)
        {
            baseMap.SetTile(point, tiles.peakTile);
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
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x - chunkSize, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x + chunkSize, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x - chunkSize, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x, currChunk.y + chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x + chunkSize, currChunk.y + chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x + chunkSize, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x - chunkSize, currChunk.y + chunkSize));
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
            TileInfo tI = new TileInfo
            {
                isTreeOn = true,
                tileName = "orangeSmallTree"
            };
            tileInfo.Add(point, tI);
        }
            else if (randomNum == 3)
            {
                //tI.isTreeOn = true;
                extraMapCollide.SetTile(point, tiles.deadTreeSmallTile);
                TileInfo tI = new TileInfo
                {
                    isTreeOn = true,
                    tileName = "deadTreeSmallTile"
                };
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
                TileInfo tI = new TileInfo
                {
                    isTreeOn = true,
                    tileName = "tealSmallEvergreenTile"
                };
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
                player.transform.position = new Vector3Int(point.x,point.y,-10);
                playerPlaced = true;
        }
    }
    private void GenerateDeepWaterTile(Vector3Int point)
    {
        Color color = new Color(0.7f, 0.9f, 0.95f);
        baseMap.SetTile(point, tiles.waterRuleTile);
        baseMap.SetColor(point, color);
        
        int randomNum = Random.Range(0, 100);
        if (randomNum == 0)
        {
            //add ti info here
            TileInfo tI = new TileInfo
            {
                tileName = "rockOnWater"
            };
            tileInfo.Add(point, tI);
            extraMapCollide.SetTile(point, tiles.rockOnWaterGray1Tile);
            extraMapNonCollide.SetTile(point, tiles.rockOnWaterGray1Tile);
            extraMapNonCollide.SetColor(point, color);
        }
        else if (randomNum == 1 || randomNum == 2)
        {
            //add ti info here
            extraMapCollide.SetTile(point, tiles.lilyPadOnWaterTile);
            TileInfo tI = new TileInfo
            {
                tileName = "lilyPad"
            };
            tileInfo.Add(point, tI);

        }
        else
        {
            extraMapCollide.SetTile(point, tiles.animatedWaterTile);
            extraMapCollide.SetColor(point, color);
        }
    }
}
