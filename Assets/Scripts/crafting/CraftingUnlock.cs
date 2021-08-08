using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, bool> recipeUnlocked;
    public Dictionary<string, int> numItemsTotal;
    public GameObject[] itemHolders;
    Dictionary<string, ItemRequirements[]> recipeRequirements;
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

    private void Start()
    {
        Instance.recipeRequirements = new CraftingRequirements().GetRequirements();
        recipeUnlocked = new Dictionary<string, bool>()
        {
            {"craftingTable", false},
            {"woodenAxe", false }
        };
    }

    public void UpdateInventory()
    {
        itemHolders = GameObject.Find("Player").GetComponent<PlayerInventory>().itemHolders;
        numItemsTotal.Clear();
        Debug.Log("Update inventory");
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

    public void CheckUnlocks()
    {
        for(int i = 0; i < Instance.recipeRequirements.Count; i ++)
        {
            for(int p = 0; p < Instance.recipeRequirements.ElementAt(i).Value.Length; p++)
            {
                Debug.Log(Instance.recipeRequirements.ElementAt(i).Value[p].id);
            }
        }
    }
    //// Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            UpdateInventory();
            CheckUnlocks();
            GameObject popUp = Instantiate(Resources.Load("Prefabs/ItemNotification")) as GameObject;
            ItemAddNotification itemNotification = popUp.GetComponentInChildren<ItemAddNotification>();
            itemNotification.AddItem("woodenAxe");
        }

    }
}
