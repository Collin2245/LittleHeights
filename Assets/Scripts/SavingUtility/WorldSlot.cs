using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WorldSlot : MonoBehaviour
{
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
            MasterWorldSave s = LoadHelper.GetMasterWorldSave(Directory.GetFiles(Application.persistentDataPath + "/Worlds/")[slotNum]);
            PersistentData.Instance.CurrentWorld = s;

            Debug.Log(PersistentData.Instance.CurrentWorld.name);
        }
    }
}
