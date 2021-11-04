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
    // Start is called before the first frame update
    void Start()
    {
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
            if(File.Exists(UpdateCharacterSavePathFile("test")))
            {
                Debug.Log("File exists here");
                Debug.Log(File.ReadAllText(saveFilePath));
            }else
            {
                Debug.Log("File does not exist here");
                SaveJsonByteArray(saveFilePath,GetJsonCharacterByteArray());
            }
        }
        
    }

    string UpdateCharacterSavePathFile(string name)
    {
        saveFilePath = Application.persistentDataPath + "/Characters/" + name + ".data";
        return saveFilePath;
    }

    byte[] GetJsonCharacterByteArray()
    {
        return Encoding.Default.GetBytes(GameObject.Find("Human").GetComponent<Character4DBase>().ToJson());
    }

    void SaveJsonByteArray(string path, byte[] byteArray)
    {
        System.IO.File.WriteAllBytes(path, byteArray);
    }
}
