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

[System.Serializable]
public class LoadHelper : MonoBehaviour
{
    MasterSave masterSave;
    string charactersFilePath;
    
    // Start is called before the first frame update
    private void Start()
    {
        charactersFilePath = Application.persistentDataPath + "/Characters/";
        if (!Directory.Exists(charactersFilePath))
        {
            Directory.CreateDirectory(charactersFilePath);

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.PageUp))
        {
            string[] characters = Directory.GetFiles(charactersFilePath);
            masterSave = LoadSave(File.ReadAllText(characters[0]));
        }
    }

    Character4DBase ResolveCharacter()
    {
        return new Character4D();
    }

    MasterSave LoadSave(string json)
    { 
        return JsonUtility.FromJson<MasterSave>(json);
    }
}
