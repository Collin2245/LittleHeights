using Assets.HeroEditor4D.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MasterSave
{
    public SaveObject saveObject;
}
[System.Serializable]
public struct SaveObject
{
    public string characterName;
    public string jsonTest;
}
