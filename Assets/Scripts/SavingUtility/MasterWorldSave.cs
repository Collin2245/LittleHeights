using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MasterWorldSave
{
    public string name;
    public float seed;
    public Dictionary<string,Vector3> CharacterToWorldPos;
}
