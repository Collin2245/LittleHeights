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
public class SaveHelper : MonoBehaviour
{
    public string saveFilePath;
    public string jsonString;
    SaveObject saveObject;
    // Start is called before the first frame update
    void Start()
    {
        saveObject = new SaveObject();
        saveObject.characterName = "Collin Krueger";
        
        if(!Directory.Exists(Application.persistentDataPath + "/Characters/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Characters/");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Home))
        {
            jsonString = GameObject.Find("Human").GetComponent<Character4DBase>().ToJson();
            saveObject.characterName = "Collin Krueger";
            //saveObject.jsonTest = jsonString;
            if (File.Exists(UpdateCharacterSavePathFile("test")))
            {
                File.WriteAllText(saveFilePath, "{\"saveObject\":" + JsonUtility.ToJson(saveObject) + ",\"characterInfo\":" + jsonString +"}");
                Debug.Log(File.ReadAllText(saveFilePath));
            }
            else
            {
                Debug.Log("File does not exist here");
                //SaveJsonByteArray(saveFilePath,JsonToByteArray(jsonString));
                JsonUtility.ToJson(saveObject);
            }
        }
        
    }

    string UpdateCharacterSavePathFile(string name)
    {
        saveFilePath = Application.persistentDataPath + "/Characters/" + name + ".data";
        return saveFilePath;
    }

    byte[] JsonToByteArray(string json)
    {
        return Encoding.Default.GetBytes(json);
    }

    void SaveJsonByteArray(string path, byte[] byteArray)
    {
        System.IO.File.WriteAllBytes(path, byteArray);
    }
}
