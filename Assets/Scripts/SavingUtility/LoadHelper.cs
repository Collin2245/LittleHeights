using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.HeroEditor4D.Common.CharacterScripts;
using Assets.HeroEditor4D.Common.CommonScripts;
using Assets.HeroEditor4D.FantasyInventory.Scripts.Data;
using Assets.HeroEditor4D.FantasyInventory.Scripts.Interface.Elements;
using Assets.HeroEditor4D.Common.SimpleColorPicker.Scripts;
using System.Text;
using HeroEditor4D.Common;
using Newtonsoft.Json;

[System.Serializable]
public class LoadHelper : MonoBehaviour
{
    MasterSave masterSave;
    string charactersFilePath;
    Character4D character4D;

    // Start is called before the first frame update
    private void Start()
    {
        charactersFilePath = Application.persistentDataPath + "/Characters/";
        if (!Directory.Exists(charactersFilePath))
        {
            Directory.CreateDirectory(charactersFilePath);
        }
    }

    private void SetCharacter()
    {
        character4D = GameObject.Find("Human").GetComponent<Character4D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.PageUp))
        {
            string[] characters = Directory.GetFiles(charactersFilePath);
            masterSave = LoadCharacterSave(File.ReadAllText(characters[0]));
            string test = JsonConvert.SerializeObject(masterSave.characterJson.characterJson);
            GameObject.Find("Human").GetComponent<Character4DBase>().FromJson(JsonConvert.SerializeObject(masterSave.characterJson.characterJson),false);
        }
    }

    public static string[] GetCharacterSaves()
    {
        return Directory.GetFiles(Application.persistentDataPath + "/Characters/");
    }

    public static string[] GetWorldSaves()
    {
        return Directory.GetFiles(Application.persistentDataPath + "/Worlds/");
    }

    public static MasterSave GetMasterSave(string path)
    {
        return  LoadCharacterSave(File.ReadAllText(path));
    }

    public static MasterWorldSave GetMasterWorldSave(string path)
    {
        return LoadWorldSave(File.ReadAllText(path));
    }

    Character4D ResolveCharacter()
    {
        Character4D character = new Character4D();
        //character = character.FromJson();
        return new Character4D();
    }

    static MasterSave LoadCharacterSave(string json)
    { 
        return JsonConvert.DeserializeObject<MasterSave>(json);
    }

    static MasterWorldSave LoadWorldSave(string json)
    {
        return JsonConvert.DeserializeObject<MasterWorldSave>(json);
    }
}
