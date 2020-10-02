using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInventorySlot : MonoBehaviour
{
    public string itemIdOnMouse;
    public bool itemOnMouse = false;
    public GameObject itemPrefabOnMouse;
    public static MouseInventorySlot Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("creating Mouse inventory slot");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (itemOnMouse)
        {
            itemPrefabOnMouse = transform.GetChild(0).gameObject;
        }
    }
}
