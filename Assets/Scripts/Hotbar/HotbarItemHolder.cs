using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarItemHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject itemHolderOnInventory;
    public GameObject itemPrefab;
    public Item item;
    public int slotNum;
    public int currAmount;
    public string itemId;
    void Start()
    {
        switch(slotNum)
        {
            case 2:
                itemHolderOnInventory = GameObject.Find("InventorySlot (24)");
                break;
            case 3:
                itemHolderOnInventory = GameObject.Find("InventorySlot (25)");
                break;
            case 4:
                itemHolderOnInventory = GameObject.Find("InventorySlot (26)");
                break;
            case 5:
                itemHolderOnInventory = GameObject.Find("InventorySlot (27)");
                break;
            case 6:
                itemHolderOnInventory = GameObject.Find("InventorySlot (28)");
                break;
            case 7:
                itemHolderOnInventory = GameObject.Find("InventorySlot (29)");
                break;
        }
        generateHotbarItem();
    }

    public void generateHotbarItem()
    {
        if(itemHolderOnInventory.GetComponent<InventorySlot>().itemId != "")
        {
            if(this.transform.childCount == 0)
            {
                itemPrefab = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, transform);
                item = itemPrefab.GetComponent<Item>();
                item.id = itemHolderOnInventory.GetComponent<InventorySlot>().itemId;
                item.currAmount = itemHolderOnInventory.GetComponent<InventorySlot>().currAmount;
            }
            item.id = itemHolderOnInventory.GetComponent<InventorySlot>().itemId;
            item.currAmount = itemHolderOnInventory.GetComponent<InventorySlot>().currAmount;
        }else
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        generateHotbarItem();
    }
}
