using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSaveDropDown : MonoBehaviour
{
    [SerializeField]
    GameObject content;
    MasterWorldSave masterWorldSave;

    void Start()
    {
        LoadDDL();
    }

    void LoadDDL()
    {
        string[] saves = LoadHelper.GetWorldSaves();
        for (int i = 0; i < saves.Length; i++)
        {
            masterWorldSave = LoadHelper.GetMasterWorldSave(saves[i]);
            Debug.Log(saves[i]);
            GenerateWorldSlot(masterWorldSave.name, 0f, i+1);
        }
        Instantiate(Resources.Load<GameObject>("Prefabs/NewWorldSlot"), content.transform);
    }

    void GenerateWorldSlot(string name, float time, int num)
    {
        var worldSlot = Instantiate(Resources.Load<GameObject>("Prefabs/WorldSlot"), content.transform);
        //SaveSlotToMasterSave.Add(saveSlot, currMasterSave);
        //saveSlot.transform.SetParent(content.transform, false);
        worldSlot.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
        worldSlot.transform.Find("NamePlaceholder").gameObject.GetComponent<Text>().text = name;
        //saveSlot.transform.Find("GoldPlaceholder").gameObject.GetComponent<Text>().text = gold.ToString();
        //saveSlot.transform.Find("LevelPlaceholder").gameObject.GetComponent<Text>().text = level.ToString();
        worldSlot.transform.Find("SlotNumBackground").gameObject.transform.Find("SlotNum").gameObject.GetComponent<Text>().text = num.ToString();
        //if (num == 1)
        //{
        //    //set default selcted first save
        //    Toggle t = saveSlot.GetComponent<Toggle>();
        //    MasterSave s = LoadHelper.GetMasterSave(Directory.GetFiles(Application.persistentDataPath + "/Characters/")[0]);
        //    PersistentData.Instance.CurrentSave = s;
        //}

    }
}
