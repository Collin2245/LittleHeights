using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    GameObject inventoryGameObject;
    GameObject hotbarGameObject;
    Canvas inventoryCanvas;
    Canvas hotbarCanvas;
    Item testItem;
    Item templateItem;
    public Item[][] inventoryArray;
    // Start is called before the first frame update


    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("creating inventory instance");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        hotbarGameObject = GameObject.Find("Hotbar");
        inventoryGameObject = GameObject.Find("InventoryCanvas");
        inventoryCanvas = inventoryGameObject.GetComponent<Canvas>();
        hotbarCanvas = hotbarGameObject.GetComponent<Canvas>();
        //Setting up blank inventory
        inventoryCanvas.enabled = false;
        hotbarCanvas.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryCanvas.enabled == true)
            {
                inventoryCanvas.enabled = false;
                hotbarCanvas.enabled = true;
            }
            else
            {
                inventoryCanvas.enabled = true;
                hotbarCanvas.enabled = false;
            }
        }   
    }
}
