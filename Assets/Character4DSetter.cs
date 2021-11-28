using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor4D;
using HeroEditor4D.Common;
using Newtonsoft.Json;

public class Character4DSetter : MonoBehaviour
{
    // Start is called before the first frame update
    Character4DBase character4DBase;
    void Start()
    {
        character4DBase = this.GetComponent<Character4DBase>();
        string jsonString =  JsonConvert.SerializeObject(PersistentData.Instance.CurrentSave.characterJson.characterJson);
        character4DBase.FromJson(jsonString, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
