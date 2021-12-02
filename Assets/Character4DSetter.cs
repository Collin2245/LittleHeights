using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor4D;
using HeroEditor4D.Common;
using Newtonsoft.Json;
using Assets.HeroEditor4D.Common.CharacterScripts;

public class Character4DSetter : MonoBehaviour
{
    // Start is called before the first frame update
    Character4D character4D;
    void Start()
    {
        character4D = this.GetComponent<Character4D>();
        string jsonString =  JsonConvert.SerializeObject(PersistentData.Instance.CurrentSave.characterJson.characterJson);
        character4D.FromJson(jsonString, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
