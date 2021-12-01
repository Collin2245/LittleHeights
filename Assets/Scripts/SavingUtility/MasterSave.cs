using Assets.HeroEditor4D.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MasterSave
{
    public SaveObject saveObject;
    public CharacterJson characterJson;
    public CharacterInventory characterInventory;
}
[System.Serializable]
public struct SaveObject
{
    public string characterName;
    public string guid;
}
[System.Serializable]
public struct CharacterJson
{
    public Dictionary<string,string> characterJson;
}

[System.Serializable]
public struct CharacterInventory
{
    public Dictionary<int, InventorySaveItem> InventoryDict;
}

[System.Serializable]
public struct InventorySaveItem
{
    public string name;
    public int count;
    public InventorySaveItem(string nameInput, int countInput)
    {
        name = nameInput;
        count = countInput;
    }
}
