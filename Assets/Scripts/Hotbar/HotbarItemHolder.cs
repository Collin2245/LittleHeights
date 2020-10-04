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
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        if (this.itemHolderOnInventory.GetComponent<InventorySlot>().itemId != "")
        {
            itemPrefab = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, transform);
            item = itemPrefab.GetComponent<Item>();
            item.id = this.itemHolderOnInventory.GetComponent<InventorySlot>().itemId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            generateHotbarItem();
        }
    }
}
