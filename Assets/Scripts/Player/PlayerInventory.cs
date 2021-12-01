﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public HotbarItemHolder currentItemHolder;
    public LittleHeightsItem currentItem;
    public GameObject itemHolder1;
    public GameObject itemHolder2;
    public GameObject itemHolder3;
    public GameObject itemHolder4;
    public GameObject itemHolder5;
    public GameObject itemHolder6;
    public GameObject itemHolder7;
    public GameObject itemHolder8;
    public GameObject itemHolder9;
    public GameObject itemHolder10;
    public GameObject itemHolder11;
    public GameObject itemHolder12;
    public GameObject itemHolder13;
    public GameObject itemHolder14;
    public GameObject itemHolder15;
    public GameObject itemHolder16;
    public GameObject itemHolder17;
    public GameObject itemHolder18;
    public GameObject itemHolder19;
    public GameObject itemHolder20;
    public GameObject itemHolder21;
    public GameObject itemHolder22;
    public GameObject itemHolder23;
    public GameObject itemHolder24;
    public GameObject itemHolder25;
    public GameObject itemHolder26;
    public GameObject itemHolder27;
    public GameObject itemHolder28;
    public GameObject itemHolder29;
    public GameObject itemHolder30;

    public GameObject[] itemHolders;
    AudioSource audioSource;
    
    //GameObject hotbar;
    void awake()
    {
        currentItemHolder = GameObject.Find("CurrentItemSelector").GetComponent<CurrentItem>().currentSlot;
    }
    void Start()
    {
        itemHolders = new GameObject[]
        {
            itemHolder1,
            itemHolder2,
            itemHolder3,
            itemHolder4,
            itemHolder5,
            itemHolder6,
            itemHolder7,
            itemHolder8,
            itemHolder9,
            itemHolder10,
            itemHolder11,
            itemHolder12,
            itemHolder13,
            itemHolder14,
            itemHolder15,
            itemHolder16,
            itemHolder17,
            itemHolder18,
            itemHolder19,
            itemHolder20,
            itemHolder21,
            itemHolder22,
            itemHolder23,
            itemHolder24,
            itemHolder25,
            itemHolder26,
            itemHolder27,
            itemHolder28,
            itemHolder29,
            itemHolder30,
        };
        audioSource = this.GetComponent<AudioSource>();
        if (PersistentData.Instance.CurrentSave.characterInventory.InventoryDict == null)
        {
            //create inventory
            PopulatePersistentData();
        }
        else
        {
            //load inventory
            LoadInventory();
        }
        SaveInventory();
    }
    void SaveInventory()
    {
        PopulatePersistentData();
        SaveHelper.SaveCharacter(PersistentData.Instance.CurrentSave);
    }
    void LoadInventory()
    {
        for (int i = 0; i < PersistentData.Instance.CurrentSave.characterInventory.InventoryDict.Count; i++)
        {
            if (PersistentData.Instance.CurrentSave.characterInventory.InventoryDict[i].count > 0)
            {
                LittleHeightsItem temp = new LittleHeightsItem();
                temp.id = PersistentData.Instance.CurrentSave.characterInventory.InventoryDict[i].name;
                temp.currAmount = PersistentData.Instance.CurrentSave.characterInventory.InventoryDict[i].count;
                AddItemToIndex(i, temp);
            }
        }
    }
    void PopulatePersistentData()
    {
        InventorySaveItem blankItem = new InventorySaveItem("",0);
        PersistentData.Instance.CurrentSave.characterInventory.InventoryDict = new Dictionary<int, InventorySaveItem>();
        for(int i = 0; i < itemHolders.Length; i ++)
        {
            if(itemHolders[i].transform.childCount > 0)
            {
                LittleHeightsItem temp;
                temp = itemHolders[i].GetComponentInChildren<LittleHeightsItem>();

                PersistentData.Instance.CurrentSave.characterInventory.InventoryDict.Add(i, new InventorySaveItem(temp.id, temp.currAmount));
            }
            else
            {
                PersistentData.Instance.CurrentSave.characterInventory.InventoryDict.Add(i, blankItem);
            }  
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentItemHolder = GameObject.Find("CurrentItemSelector").GetComponent<CurrentItem>().currentSlot;
        if(currentItemHolder.transform.childCount > 0)
        {
            currentItem = currentItemHolder.GetComponentInChildren<LittleHeightsItem>();
        }
        else
        {
            currentItem = null;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            SaveInventory();
        }
        
    }

    public void TryToAddItemToInventory(LittleHeightsItem item)
    {
        if(TryToAddItemToExistingItem(item))
        {
            //Debug.Log("Picked up existing item and added to count");
            Destroy(item.transform.gameObject);
            audioSource.pitch = Random.Range(0.9f, 1.5f);
            audioSource.PlayOneShot(audioSource.clip);
            return;
        }
        if(TryToAddItemToEmptySlot(item))
        {
            //Debug.Log("Added new item to inventory");
            Destroy(item.transform.gameObject);
            audioSource.pitch = Random.Range(0.9f, 1.5f);
            audioSource.PlayOneShot(audioSource.clip);
            return;
        }
        Debug.Log("Inventory is full");
    }

    public void TryToAddItemToInventoryNonDroppedItem(LittleHeightsItem item)
    {
        if (TryToAddItemToExistingItem(item))
        {
            //Debug.Log("Picked up existing item and added to count");
            return;
        }
        if (TryToAddItemToEmptySlot(item))
        {
            //Debug.Log("Added new item to inventory");
            return;
        }
        Debug.Log("Inventory is full");
    }

    public void AddItemToIndex(int i, LittleHeightsItem item)
    {
        //itemHolders[i].gameObject.GetComponent<InventorySlot>().GetComponentInChildren<Item>().currAmount = item.currAmount;
        GameObject temp = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, itemHolders[i].transform);
        LittleHeightsItem tempItem = temp.GetComponent<LittleHeightsItem>();
        tempItem.id = item.id;
        tempItem.currAmount = item.currAmount;
        //need to set inventoryslot stuff
        itemHolders[i].GetComponent<InventorySlot>().itemId = item.id;
        itemHolders[i].GetComponent<InventorySlot>().currAmount = item.currAmount;
        //Debug.Log("Inventory is full");
    }

    private bool TryToAddItemToExistingItem(LittleHeightsItem item)
    {
        for(int i = 0; i < this.itemHolders.Length; i++)
        {
            if(itemHolders[i].GetComponent<InventorySlot>().itemId == "")
            {
                continue;
            }
            else if(itemHolders[i].GetComponent<InventorySlot>().itemId == item.id)
            {
                if(itemHolders[i].GetComponent<InventorySlot>().currAmount + item.currAmount <= ItemProperties.quantityForItem[item.id])
                {
                    itemHolders[i].GetComponent<InventorySlot>().GetComponentInChildren<LittleHeightsItem>().currAmount += item.currAmount;
                    itemHolders[i].GetComponent<InventorySlot>().generateInventory();
                    return true;
                }
                Debug.Log("Found matching, but max quantity has been reached");
                continue;
            }
            else
            {
                continue;
            } 
        }
        return false;
    }

    private bool TryToAddItemToEmptySlot(LittleHeightsItem item)
    {
        for (int i = 0; i < this.itemHolders.Length; i++) 
        {
            if (itemHolders[i].GetComponent<InventorySlot>().itemId == "")
            {
                ////instantiate!!!!
                //itemHolders[i].gameObject.GetComponentInChildren<Item>().id = item.id;
                //itemHolders[i].gameObject.GetComponent<InventorySlot>().GetComponentInChildren<Item>().currAmount = item.currAmount;
                GameObject temp = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, itemHolders[i].transform);
                LittleHeightsItem tempItem = temp.GetComponent<LittleHeightsItem>();
                tempItem.id = item.id;
                tempItem.currAmount = item.currAmount;
                //need to set inventoryslot stuff
                itemHolders[i].GetComponent<InventorySlot>().itemId = item.id;
                itemHolders[i].GetComponent<InventorySlot>().currAmount = item.currAmount;
                return true;
            }
        }
        return false;
    }

    public void TryToRemoveItemFromInventory()
    {
        currentItem.currAmount -=1;
    }
}
