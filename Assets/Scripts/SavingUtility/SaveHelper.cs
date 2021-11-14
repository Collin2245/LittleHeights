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
using UnityEngine.UI;
using System;

[System.Serializable]
public class SaveHelper : MonoBehaviour
{
    public string saveFilePath;
    public string jsonString;
    string characterPath;
    SaveObject saveObject;
    MasterSave masterSave;
    CharacterJson characterJson;
    [SerializeField] 
    GameObject ButtonObj;
    [SerializeField]
    GameObject TextObj;

    // Start is called before the first frame update
    void Start()
    {
        saveObject = new SaveObject();
        masterSave = new MasterSave();
        characterPath = Application.persistentDataPath + "/Characters/";
        if (!Directory.Exists(Application.persistentDataPath + "/Characters/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Characters/");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/Worlds/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Worlds/");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TextObj != null && ButtonObj != null)
        {
            if (!(TextObj.GetComponent<Text>().text == ""))
            {
                ButtonObj.GetComponent<Button>().interactable = true;
            }else
            {
                ButtonObj.GetComponent<Button>().interactable = false;
            }
        }
    }

    void SaveName(string name)
    {
        saveObject.characterName = name;
    }    

    public void NewSave()
    {
        int currSaves = Directory.GetFiles(characterPath).Length;
        saveObject.saveSlot = currSaves + 1;
        saveObject.guid = Guid.NewGuid().ToString();
        Guid test = Guid.Parse(saveObject.guid);
        SaveMasterSave(UpdateCharacterSavePathFile((saveObject.guid).ToString()));
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

    void SaveMasterSave(string path)
    {
        characterJson.characterJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(GameObject.Find("Human").GetComponent<Character4DBase>().ToJson());
        if (File.Exists(path))
        {
            masterSave.saveObject = saveObject;
            masterSave.characterJson.characterJson = characterJson.characterJson;
            File.WriteAllBytes(saveFilePath, Encoding.Default.GetBytes(JsonConvert.SerializeObject(masterSave)));
            Debug.Log(File.ReadAllText(saveFilePath));
        }
        else
        {
            Debug.Log("File does not exist here");
            //add input validation here
            saveObject.characterName = GameObject.Find("CharacterName").GetComponent<Text>().text;
            masterSave.saveObject = saveObject;
            masterSave.characterJson.characterJson = characterJson.characterJson;
            File.WriteAllBytes(saveFilePath, Encoding.Default.GetBytes(JsonConvert.SerializeObject(masterSave)));
            Debug.Log(File.ReadAllText(saveFilePath));
        }
    }

}
