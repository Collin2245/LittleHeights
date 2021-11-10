using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSaveDropDown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject content;
    [SerializeField]
    Vector3 startingPos;

    void Start()
    {
        //startingPos = new Vector3(-0.4f, 381.7f, 0);
        startingPos = new Vector3(-1, 1, 0);
        LoadDDL();
    }

    void LoadDDL()
    {
        string[] saves = LoadHelper.GetSaves();
        for (int i = 0; i < saves.Length; i++)
        {
            MasterSave masterSave = LoadHelper.GetMasterSave(saves[i]);
            Debug.Log(saves[i]);
            GenerateSaveSlot(masterSave.saveObject.characterName, 250, 1, i+1, startingPos);
            //startingPos = new Vector3(startingPos.x, startingPos.y + verticalSpacing, startingPos.z);
            //content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y + Mathf.Abs(verticalSpacing));

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (content.GetComponent<RectTransform>().sizeDelta != new Vector2(0, 180))
        //{
        //    content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 180);
        //}
    }

    void GenerateSaveSlot(string name, int gold, int level, int num, Vector3 pos)
    {
        var saveSlot = Instantiate(Resources.Load<GameObject>("Prefabs/SaveSlot"),pos,Quaternion.identity);
        saveSlot.transform.SetParent(content.transform,false);
        GameObject nameText = saveSlot.transform.Find("NamePlaceholder").gameObject;
        GameObject goldText = saveSlot.transform.Find("GoldPlaceholder").gameObject;
        GameObject lvlText = saveSlot.transform.Find("LevelPlaceholder").gameObject;
        GameObject slotNum = saveSlot.transform.Find("SlotNumBackground").gameObject.transform.Find("SlotNum").gameObject;
        saveSlot.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
        nameText.GetComponent<Text>().text = name;
        goldText.GetComponent<Text>().text = gold.ToString();
        lvlText.GetComponent<Text>().text = level.ToString();
        slotNum.GetComponent<Text>().text = num.ToString();
    }
}
