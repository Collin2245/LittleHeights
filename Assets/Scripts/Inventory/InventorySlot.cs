﻿using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public string itemId;
    private string path;
    private bool itemHasBeenDrawn;
    public GameObject itemPrefab;
    //public GameObject MouseInventorySlot.Instance.itemPrefabOnMouse;
    private RectTransform rectTransform;
    private Button btn;
    public Item item;
    public string itemMovement;
    public GameObject mouseInventory;
    public int currAmount;
    void Start()
    {
        mouseInventory = GameObject.Find("MouseInventorySlot");
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => InventoryButtonClicked());
        rectTransform = GetComponent<RectTransform>();
        generateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseInventorySlot.Instance.itemOnMouse) //item follow mouse
        {
            if (MouseInventorySlot.Instance.transform.childCount == 0)
            {
                MouseInventorySlot.Instance.itemPrefabOnMouse = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, MouseInventorySlot.Instance.transform);
                MouseInventorySlot.Instance.itemPrefabOnMouse.GetComponent<Item>().id = MouseInventorySlot.Instance.itemIdOnMouse;
                //MouseInventorySlot.Instance.itemPrefabOnMouse.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                MouseInventorySlot.Instance.itemPrefabOnMouse.transform.position = new Vector3(Input.mousePosition.x + 60, Input.mousePosition.y - 60, Input.mousePosition.z);
            }

        }
        generateInventory();
    }

    void generateInventory()
    {
        if (itemId != "")
        {
            if (this.transform.childCount == 0)
            {
                itemPrefab = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, rectTransform);
                item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
            }
            else
            {
                path = "Items/" + this.itemId;
                transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(path);
            }
        }
    }

    void InventoryButtonClicked()
    {
        if(MouseInventorySlot.Instance.itemOnMouse)//clicked when item is on mouse
        {
            if(itemId == "")
            {
                itemId = MouseInventorySlot.Instance.itemIdOnMouse;
                itemPrefab = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, rectTransform);
                item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
                Destroy(MouseInventorySlot.Instance.itemPrefabOnMouse);
                MouseInventorySlot.Instance.itemIdOnMouse = "";
                MouseInventorySlot.Instance.itemOnMouse = false;
                foreach (Transform child in MouseInventorySlot.Instance.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                string tempItemId = itemId;
                itemId = MouseInventorySlot.Instance.itemIdOnMouse;
                item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
                Item tempItem = MouseInventorySlot.Instance.GetComponentInChildren<Item>();
                tempItem.id = tempItemId;
                MouseInventorySlot.Instance.itemIdOnMouse = tempItemId;
            }
        }
        else //first click
        {
            if(itemId == "")
            {
                return;
            }
            else //first click with something in there
            {
                Debug.Log("Clicked item with no item on mouse and valid item in slot");
                //make this another function
                MouseInventorySlot.Instance.itemIdOnMouse = itemId;
                MouseInventorySlot.Instance.itemOnMouse = true;
                itemId = "";
                item.id = "";
                MouseInventorySlot.Instance.itemPrefabOnMouse = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, MouseInventorySlot.Instance.transform);
                Item tempItem = MouseInventorySlot.Instance.GetComponentInChildren<Item>();
                tempItem.id = itemId;
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        generateInventory();
    }
}