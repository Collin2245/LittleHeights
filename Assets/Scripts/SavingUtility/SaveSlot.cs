using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    // Start is called before the first frame update
    Toggle toggle;
    void Start()
    {
        toggle = this.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(checkToggle);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void checkToggle(bool o)
    {
        if (toggle.isOn)
        {
            int slotNum = Int32.Parse(this.gameObject.transform.Find("SlotNumBackground").transform.Find("SlotNum").GetComponent<Text>().text) - 1;
            MasterSave s = LoadHelper.GetMasterSave(Directory.GetFiles(Application.persistentDataPath + "/Characters/")[slotNum]);
            PersistentData.Instance.CurrentSave = s;
            
            Debug.Log(PersistentData.Instance.CurrentSave.saveObject.characterName);
        }
    }
}
