using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance { get; private set; }
    public MasterSave CurrentSave;
    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("creating persistent data instance");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
