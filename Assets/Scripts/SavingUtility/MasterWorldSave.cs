using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MasterWorldSave
{
    public string name;
    public float seed;
    public string guid;
    public Dictionary<string, MocVector3> CharacterToWorldPos;
    //public Dictionary<Vector3Int, TileInfo> tileInfo;
    //public Dictionary<Vector2Int, bool> drawnChunks;
}

[System.Serializable]
public struct MocVector3
{
    public float x;
    public float y;
    public float z;
}
