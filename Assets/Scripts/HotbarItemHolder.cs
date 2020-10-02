using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarItemHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject itemHolderOnInventory;
    public int slotNum;
    void Start()
    {
        switch(slotNum)
        {
            case 1:
                itemHolderOnInventory = GameObject.Find("InventorySlot (23)");
                break;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
