using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTab : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, bool> recipeUnlocked;
    public Dictionary<string, int> numItemsTotal;
    public GameObject[] itemHolders;
    Dictionary<string, ItemRequirements[]> recipeRequirements;
    public GameObject[] CategoryBoxes;
    public GameObject[] ItemToCraftBoxes;
    public GameObject ItemDesc;
    public GameObject ItemToCraft;
    public GameObject CraftButton;
    public GameObject ItemName;
    public string[] CategoryArrayName;
    public string CurrentItem;
    public static CraftingTab Instance { get; private set; }

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
        Instance.recipeRequirements = CraftingProperties.GetRequirements();
        Instance.CategoryArrayName = CraftingProperties.categoryNames;
        recipeUnlocked = new Dictionary<string, bool>()
        {
            {"craftingTable", false},
            {"woodenAxe", false }
        };
        InitializeCategoryBoxes();
        SetItemInfo(CurrentItem);
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
                for(int z = 0; z< Instance.recipeUnlocked.Count; z++)
                {
                    if(Instance.recipeUnlocked.ElementAt(z).Value == false)
                    {
                        //Instance.recipeRequirements.ElementAt(i).;
                    }
                }
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
            ShowPopUp("acorn");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            
        }

    }

    void InitializeCategoryBoxes()
    {
        for(int i = 0; i < CategoryBoxes.Length; i++)
        {
            try
            {
                ItemCategory iC = CategoryBoxes[i].GetComponent<ItemCategory>();
                iC.CategoryName = CategoryArrayName[i];
                iC.GenerateImage();
                if(i == 0)
                {
                    InitializeItemsToCraft(iC.CategoryName);
                }
            }
            catch
            {
                Debug.LogException(new System.Exception("No category available for this name"));
            }
        }
    }

    void InitializeItemsToCraft(string categoryName)
    {
        for (int i = 0; i < ItemToCraftBoxes.Length; i++)
        {
            try
            {
                ItemToCraft iC = ItemToCraftBoxes[i].GetComponent<ItemToCraft>();
                iC.ItemName = CraftingProperties.categoyItems[categoryName][i];
                iC.GenerateImage();
                if( i == 0)
                {
                    CurrentItem = iC.ItemName;
                }
            }
            catch
            {
                Debug.LogException(new System.Exception("No category available for this name"));
            }
        }
    }

    void SetItemInfo(string CurrentItemName)
    {
        ItemDesc.GetComponent<TextMeshProUGUI>().text = ItemProperties.itemDescription[CurrentItemName];
        ItemName.GetComponent<TextMeshProUGUI>().text = CurrentItemName;
        ItemToCraft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + CurrentItemName);
    }

    void ShowPopUp(string itemName)
    {
        GameObject popUp = Instantiate(Resources.Load("Prefabs/ItemNotification")) as GameObject;
        ItemAddNotification itemNotification = popUp.GetComponentInChildren<ItemAddNotification>();
        itemNotification.AddItem(itemName);
    }
}
