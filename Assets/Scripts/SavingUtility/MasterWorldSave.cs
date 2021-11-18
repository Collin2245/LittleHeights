using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[System.Serializable]
public struct MasterWorldSave
{
    public string name;
    public float seed;
    public string guid;
    public List<KeyValuePair<string, MocVector3int>> CharacterToWorldPos;
    //public List<KeyValuePair<MocVector2int,bool>> DrawnChunks;
    public List<KeyValuePair<MocVector3int, TileInfo>> tileInfo;
}

[System.Serializable]
public struct MocVector3int
{
    public int x;
    public int y;
    public int z;

    //[JsonIgnore]
    public MocVector3int(int rX, int rY, int rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }
    //[JsonIgnore]
    public static implicit operator Vector3Int(MocVector3int rValue)
    {
        return new Vector3Int(rValue.x, rValue.y, rValue.z);
    }
    //[JsonIgnore]
    public static implicit operator MocVector3int(Vector3Int rValue)
    {
        return new MocVector3int(rValue.x, rValue.y, rValue.z);
    }
}
[System.Serializable]
public struct MocVector2int
{
    public int x;
    public int y;
    public MocVector2int(int rX, int rY)
    {
        x = rX;
        y = rY;
    }

    public static implicit operator Vector2Int(MocVector2int rValue)
    {
        return new Vector2Int(rValue.x, rValue.y);
    }

    public static implicit operator MocVector2int(Vector2Int rValue)
    {
        return new MocVector2int(rValue.x, rValue.y);
    }
}
