using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, bool> recipeUnlocked;
    public Dictionary<string, int> numItemsTotal;
    public GameObject[] itemHolders;
    public static CraftingUnlock Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("creating crafting unlocks instance");
            Instance = this;
            Instance.recipeUnlocked = new Dictionary<string, bool>();
            Instance.numItemsTotal = new Dictionary<string, int>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void grabInventory()
    {
        Debug.Log("Printing inventory totals");
        itemHolders = GameObject.Find("Player").GetComponent<PlayerInventory>().itemHolders;
        numItemsTotal.Clear();
        foreach (GameObject item in itemHolders)
        {
            InventorySlot inventorySlot = item.GetComponent<InventorySlot>();
            if(inventorySlot.itemId != "")
            {
                if(numItemsTotal.ContainsKey(inventorySlot.itemId))
                {
                    numItemsTotal[inventorySlot.itemId] = numItemsTotal[inventorySlot.itemId] + inventorySlot.currAmount;
                }
                else
                {
                    numItemsTotal[inventorySlot.itemId] = inventorySlot.currAmount;
                }
            }
        }
    }
    //// Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            grabInventory();
        }

    }
}
