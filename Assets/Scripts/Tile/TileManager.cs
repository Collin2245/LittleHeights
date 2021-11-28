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
    public Dictionary<string, TileBase> tileDict = new Dictionary<string, TileBase>();
    Dictionary<string, MocVector3int> playerPlacedDict;


    void Start()
    {
        LoadWithPersistentData();
    }

    void LoadWithPersistentData()
    {
        
        tiles = this.GetComponent<Tiles>();
        //FillTileDict();
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
        PlacePlayer();
        //DrawChunk(chunkSize, scale, seed, new Vector2Int(chunkSize * startMult, chunkSize * startMult));
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
            SaveHelper.SaveWorld(PersistentData.Instance.CurrentWorld);
        }
    }

    void UpdateInstance()
    {
        SavePlayerPos();
        ConvertToListOfList(tileInfo);
    }

    void ConvertToListOfList(Dictionary<MocVector2int, Dictionary<MocVector3int, TileInfo>> tileInfo)
    {
        //CHANGE TO FOR LOOP
        tileInfoPersistent.Clear();
        for(int i = 0; i < tileInfo.Count-1; i ++)
        {
            List<KeyValuePair<MocVector3int, TileInfo>> points = new List<KeyValuePair<MocVector3int, TileInfo>>();
            for (int y = 0; y < tileInfo.ElementAt(i).Value.Count - 1; y++)
            {
                points.Add(new KeyValuePair<MocVector3int, TileInfo>(tileInfo.ElementAt(i).Value.ElementAt(y).Key, tileInfo.ElementAt(i).Value.ElementAt(y).Value));
            }
            PersistentData.Instance.CurrentWorld.tileInfo.Add(new KeyValuePair<MocVector2int, List<KeyValuePair<MocVector3int, TileInfo>>>(tileInfo.ElementAt(i).Key, points));
        }
      
        //foreach (KeyValuePair<MocVector2int, Dictionary<MocVector3int,TileInfo >> chunk in tileInfo)
        //{
        //    List<KeyValuePair<MocVector3int, TileInfo>> points = new List<KeyValuePair<MocVector3int, TileInfo>>();
        //    foreach (KeyValuePair<MocVector3int, TileInfo> point in chunk.Value)
        //    {
        //        //points is being set for all entries of dict
        //        points.Add(new KeyValuePair<MocVector3int, TileInfo>(point.Key, point.Value));
        //    }
        //    PersistentData.Instance.CurrentWorld.tileInfo.Add(new KeyValuePair<MocVector2int, List<KeyValuePair<MocVector3int,TileInfo>>>(chunk.Key, points));
        //}
    }

    public TileInfo GetTileInfoAtPoint()
    {
        Vector3Int pointV3 = baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        MocVector3int point;
        Vector2Int chunk = GetChunk(pointV3);
        point.x = pointV3.x;
        point.y = pointV3.y;
        point.z = pointV3.z;
        float perlin = perlinAtPoint(point);
        //if (!tileInfo.)
        //{

        //    if (perlin < 0.35f)
        //    {

        //    }
        //    else if (perlin > 0.35f && perlin <= 0.4f)
        //    {

        //    }
        //    else if (perlin > 0.4f && perlin <= 0.8f)
        //    {

        //    }
        //}
        //Debug.Log("Is water?" + tileInfo[point].isWater);
        return tileInfo[chunk][point];
    }
    public TileInfo GetTileInfoAtPoint(MocVector2int chunk,  MocVector3int point)
    {
        float perlin = perlinAtPoint(point);
        if (!tileInfo[chunk].ContainsKey(point))
        {

            if (perlin < 0.35f)
            {
                TileInfo ti = new TileInfo
                {
                    isWater = true
                };
                tileInfo[chunk].Add(point, ti);
            }
            else if (perlin > 0.35f && perlin <= 0.4f)
            {
                TileInfo ti = new TileInfo
                {
                    isWalkableWater = true
                };
                tileInfo[chunk].Add(point, ti);
            }
            else if (perlin > 0.4f && perlin <= 0.8f)
            {
                TileInfo ti = new TileInfo
                {
                    isGrass = true
                };
                tileInfo[chunk].Add(point, ti);
            }



        }
        return tileInfo[chunk][point];
    }

    void DrawChunk(int chunkSize, float scale, float seed, MocVector2int chunk)
    {
        if(!drawnChunks.ContainsKey(chunk) || !drawnChunks[chunk])
        {
            drawnChunks.Add(chunk, true);
            if(tileInfo.ContainsKey(chunk))
            {
                for (int y = chunk.y; y < chunkSize + chunk.y; y++)
                {
                    for (int x = chunk.x; x < chunkSize + chunk.x; x++)
                    {
                        Vector3Int point = new Vector3Int(x, y, 0);
                        float xF = (((float)x + seed) / (float)chunkSize * scale);
                        float yF = ((float)y / (float)chunkSize * scale);
                        float perlin = Mathf.PerlinNoise(xF, yF);

                        
                        LoadTileWithPerlin(perlin, chunk, point);
                        Debug.Log(tileInfo[chunk].Keys);
                        var test = tileInfo[chunk];
                        var test2 = tileInfo[chunk].ContainsKey(point);
                      
                        var test4 = "f";
                        if(tileInfo[chunk].ContainsKey(point))
                        {
                            LoadTile(chunk, point, tileInfo[chunk][point].tileName);
                        }
                    }
                }
            }
            else
            {
                tileInfo.Add(chunk, new Dictionary<MocVector3int, TileInfo>());
                for (int y = chunk.y; y < chunkSize + chunk.y; y++)
                {
                    for (int x = chunk.x; x < chunkSize + chunk.x; x++)
                    {
                        //issue here, points saving to the rw
                        Vector3Int point = new Vector3Int(x, y, 0);
                        float xF = (((float)x + seed) / (float)chunkSize * scale);
                        float yF = ((float)y / (float)chunkSize * scale);
                        float perlin = Mathf.PerlinNoise(xF, yF);
                        
                        //add initial placement here
                        PlaceTileWithPerlin(perlin, chunk, point);
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
    private void LoadTileWithPerlin(float perlin,Vector2Int chunk, Vector3Int point)
    {
        int randomNum = Random.Range(0, 200);
        if (perlin <= 0.36f)
        {
            Color color = new Color(0.7f, 0.9f, 0.95f);
            baseMap.SetTile(point, tiles.waterRuleTile);
            baseMap.SetColor(point, color);
        }
        else if (perlin > 0.36f && perlin <= 0.4f)
        {
            baseMap.SetTile(point, tiles.waterRuleTile);
        }
        else if (perlin > 0.4f && perlin <= 0.8f)
        {
            if (randomNum % 2 == 1)
            {
                baseMap.SetTile(point, tiles.grass1Tile);
            }
            else
            {
                baseMap.SetTile(point, tiles.grass2Tile);
            }
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
            GenerateDeepWaterTile(chunk,point);
        }
        else if (perlin > 0.36f && perlin <= 0.4f)
        {
            baseMap.SetTile(point, tiles.waterRuleTile);
        }
        else if (perlin > 0.4f && perlin <= 0.8f)
        {
            GenerateGrassTile(chunk,point);
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

    Vector2Int GetChunk(Vector3Int pos)
    {
        return new Vector2Int((pos.x / chunkSize) * chunkSize, (pos.y / chunkSize) * chunkSize);
    }
    void DrawChunksAroundPlayer()
    {
        Vector2Int currChunk = GetChunkAccordingToPlayerPos();
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x, currChunk.y));

        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x + chunkSize, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x - chunkSize, currChunk.y));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x, currChunk.y + chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x - chunkSize, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x + chunkSize, currChunk.y + chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x + chunkSize, currChunk.y - chunkSize));
        DrawChunk(chunkSize, scale, seed, new MocVector2int(currChunk.x - chunkSize, currChunk.y + chunkSize));
    }

    private void GenerateGrassTile(Vector2Int chunk, Vector3Int point)
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
                tileName = "orangeTreeSmallTile"
            };
            tileInfo[chunk].Add(point, tI);
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
            tileInfo[chunk].Add(point, tI);
        }
            else if (randomNum == 4)
            {
            TileInfo tI = new TileInfo
            {
                tileName = "purpleFlowerTile"
            };
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
                tileInfo[chunk].Add(point, tI);
        }
            else if (randomNum == 7)
            {
            TileInfo tI = new TileInfo
            {
                tileName = "tealSmallBushTile"
            };
            extraMapNonCollide.SetTile(point, tiles.tealSmallBushTile);
            }
            else if (randomNum == 8)
            {
            //tI.isTreeOn = true;
            TileInfo tI = new TileInfo
            {
                tileName = "grassWithRocksTile"
            };
            extraMapNonCollide.SetTile(point, tiles.grassWithRocksTile);
            }
            else if (randomNum >=15 && playerPlaced == false)
            {
                player.transform.position = new Vector3Int(point.x,point.y,-10);
                //PersistentData.Instance.CurrentWorld.CharacterToWorldPos.Add(new KeyValuePair<string, MocVector3int>() { PersistentData.Instance.CurrentSave.saveObject.guid, })
                playerPlaced = true;
            }
    }
    private void GenerateDeepWaterTile(Vector2Int chunk, Vector3Int point)
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
                tileName = "rockOnWaterGray1Tile"
            };
            tileInfo[chunk].Add(point, tI);
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
                tileName = "lilyPadOnWaterTile"
            };
            tileInfo[chunk].Add(point, tI);

        }
        else
        {
            extraMapCollide.SetTile(point, tiles.animatedWaterTile);
            extraMapCollide.SetColor(point, color);
        }
    }

    void PlacePlayer()
    {
        playerPlacedDict = PersistentData.Instance.CurrentWorld.CharacterToWorldPos.ToDictionary(x => x.Key, x => x.Value);
        playerPlaced = playerPlacedDict.ContainsKey(PersistentData.Instance.CurrentSave.saveObject.guid);
        if(playerPlaced)
        {
            MocVector3int temp = playerPlacedDict[PersistentData.Instance.CurrentSave.saveObject.guid];
            Vector3 pos = new Vector3((float)temp.x, (float)temp.y, (float)temp.z);
            player.transform.position = pos;
        }
    }

    void SavePlayerPos()
    {
        MocVector3int pos = new Vector3Int(((int)player.transform.position.x), ((int)player.transform.position.y), ((int)player.transform.position.z));
        playerPlacedDict[PersistentData.Instance.CurrentSave.saveObject.guid] = pos;
        PersistentData.Instance.CurrentWorld.CharacterToWorldPos = playerPlacedDict.ToList();
    }

    private void LoadTile(Vector2Int chunk, Vector3Int point, string tileName)
    {
        Color color;
        switch (tileName)
        {
            case "lilyPadOnWaterTile":
                extraMapCollide.SetTile(point, tiles.lilyPadOnWaterTile);
                break;
            case "tealSmallEvergreenTile":
                extraMapCollide.SetTile(point, tiles.tealSmallEvergreenTile);
                break;
            case "deadTreeSmallTile":
                extraMapCollide.SetTile(point, tiles.deadTreeSmallTile);
                break;
            case "rockOnWaterGray1Tile":
                color = new Color(0.7f, 0.9f, 0.95f);
                extraMapCollide.SetTile(point, tiles.rockOnWaterGray1Tile);
                extraMapNonCollide.SetTile(point, tiles.rockOnWaterGray1Tile);
                extraMapNonCollide.SetColor(point, color);
                break;
            case "orangeTreeSmallTile":
                extraMapCollide.SetTile(point, tiles.orangeTreeSmallTile);
                break;
            case "purpleFlowerTile":
                extraMapNonCollide.SetTile(point, tiles.purpleFlowerTile);
                break;
            default:
                break;
        }
    }
}
