using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSaveDropDown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject content;
    Dictionary<GameObject, MasterSave> SaveSlotToMasterSave;
    MasterSave currMasterSave;

    void Start()
    {
        SaveSlotToMasterSave = new Dictionary<GameObject, MasterSave>();
        LoadDDL();
    }

    void LoadDDL()
    {
        string[] saves = LoadHelper.GetSaves();
        for (int i = 0; i < saves.Length; i++)
        {
            currMasterSave = LoadHelper.GetMasterSave(saves[i]);
            Debug.Log(saves[i]);
            GenerateSaveSlot(currMasterSave.saveObject.characterName, 250, 1, i+1);
        }
        Instantiate(Resources.Load<GameObject>("Prefabs/NewCharacterSlot"), content.transform);
    }

    void GenerateSaveSlot(string name, int gold, int level, int num)
    {
        var saveSlot = Instantiate(Resources.Load<GameObject>("Prefabs/SaveSlot"),content.transform);
        SaveSlotToMasterSave.Add(saveSlot, currMasterSave);
        saveSlot.transform.SetParent(content.transform,false);
        saveSlot.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
        saveSlot.transform.Find("NamePlaceholder").gameObject.GetComponent<Text>().text = name;
        saveSlot.transform.Find("GoldPlaceholder").gameObject.GetComponent<Text>().text = gold.ToString();
        saveSlot.transform.Find("LevelPlaceholder").gameObject.GetComponent<Text>().text = level.ToString();
        saveSlot.transform.Find("SlotNumBackground").gameObject.transform.Find("SlotNum").gameObject.GetComponent<Text>().text = num.ToString();
        if(num == 1)
        {
            //set default selcted first save
            Toggle t = saveSlot.GetComponent<Toggle>();
            MasterSave s = LoadHelper.GetMasterSave(Directory.GetFiles(Application.persistentDataPath + "/Characters/")[0]);
            PersistentData.Instance.CurrentSave = s;
        }

    }
}
