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
    string worldPath;
    SaveObject saveObject;
    MasterSave masterSave;
    CharacterJson characterJson;
    MasterWorldSave masterWorldSave;
    [SerializeField] 
    GameObject ButtonObj;
    [SerializeField]
    GameObject ButtonObjWorld;
    [SerializeField]
    GameObject TextObjCharacter;
    [SerializeField]
    GameObject TextObjWorld;

    // Start is called before the first frame update
    void Start()
    {
        saveObject = new SaveObject();
        masterSave = new MasterSave();
        masterWorldSave = new MasterWorldSave();
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
        if(TextObjCharacter != null && ButtonObj != null)
        {
            if (!(TextObjCharacter.GetComponent<Text>().text == ""))
            {
                ButtonObj.GetComponent<Button>().interactable = true;
            }else
            {
                ButtonObj.GetComponent<Button>().interactable = false;
            }
        }
        if (TextObjWorld != null && ButtonObjWorld != null)
        {
            if (!(TextObjWorld.GetComponent<Text>().text == ""))
            {
                ButtonObjWorld.GetComponent<Button>().interactable = true;
            }
            else
            {
                ButtonObjWorld.GetComponent<Button>().interactable = false;
            }
        }
    }

    void SaveName(string name, string path)
    {
        saveObject.characterName = name;
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

    public void NewSave()
    {
        int currSaves = Directory.GetFiles(characterPath).Length;
        saveObject.saveSlot = currSaves + 1;
        saveObject.guid = Guid.NewGuid().ToString();
        Guid test = Guid.Parse(saveObject.guid);
        SaveMasterSave(UpdateCharacterSavePathFile((saveObject.guid).ToString()));
    }

    public void NewWorldSave()
    {
        masterWorldSave.guid = Guid.NewGuid().ToString();
        masterWorldSave.name = TextObjWorld.GetComponent<Text>().text;
        masterWorldSave.seed = UnityEngine.Random.Range(1f, 100000f);
        masterWorldSave.tileInfo = new List<KeyValuePair<MocVector3int, TileInfo>>();
        //masterWorldSave.DrawnChunks = new List<KeyValuePair<MocVector2int, bool>>();
        masterWorldSave.CharacterToWorldPos = new List<KeyValuePair<string, MocVector3int>>();
        PersistentData.Instance.CurrentWorld = masterWorldSave;
        worldPath = Application.persistentDataPath + "/Worlds/" + masterWorldSave.guid + ".data";
        File.WriteAllBytes(worldPath, Encoding.Default.GetBytes(JsonConvert.SerializeObject(masterWorldSave)));
        Debug.Log(File.ReadAllText(worldPath));
        PersistentData.Instance.CurrentWorld = masterWorldSave;
    }

    public static void NewWorldSave(MasterWorldSave masterWorldSave)
    {
        //masterWorldSave.guid = Guid.NewGuid().ToString();
        //masterWorldSave.name = TextObjWorld.GetComponent<Text>().text;
        //masterWorldSave.seed = UnityEngine.Random.Range(1f, 100000f);
        //masterWorldSave.tileInfo = new Dictionary<MocVector3int, TileInfo>();
        //masterWorldSave.DrawnChunks = new Dictionary<MocVector2int, bool>();
        //masterWorldSave.CharacterToWorldPos = new Dictionary<string, MocVector3int>();
        PersistentData.Instance.CurrentWorld = masterWorldSave;
        string worldPath = Application.persistentDataPath + "/Worlds/" + masterWorldSave.guid + ".data";
        File.WriteAllBytes(worldPath, Encoding.Default.GetBytes(JsonConvert.SerializeObject(masterWorldSave)));
        Debug.Log(File.ReadAllText(worldPath));
        PersistentData.Instance.CurrentWorld = masterWorldSave;
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
