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
    public GameObject[] IngredientBoxes;
    public GameObject ItemDesc;
    public GameObject ItemToCraft;
    public GameObject CraftButton;
    public GameObject ItemName;
    public GameObject CraftingAmount;
    public GameObject CanCraft;
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
        recipeUnlocked = CraftingProperties.RecipeUnlocked;
        InitializeCategoryBoxes();
        SetItemInfo(CurrentItem);
        InitializeIngredients();
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


    bool CanCraftItem(string ItemName)
    {
        bool canCraft = true;
        for (int i = 0; i < Instance.recipeRequirements[ItemName].Length; i++)
        {
            if (!(canCraft
                && (numItemsTotal.ContainsKey(Instance.recipeRequirements[ItemName].ElementAt(i).id)
                && (numItemsTotal[Instance.recipeRequirements[ItemName].ElementAt(i).id] >= Instance.recipeRequirements[ItemName].ElementAt(i).amount)
                )))
            {
                canCraft = false;
            }   
        }
        return canCraft;
    }

    //add more func with this in future, for now implement a crafting method that just takes in a string and returns a bool.
    public void CheckUnlocks()
    {
        for(int i = 0; i < Instance.recipeRequirements.Count; i ++)
        {
            bool canCraft = true;
            //for every item in the game that a recipe can be unlocked...
            for(int p = 0; p < Instance.recipeRequirements.ElementAt(i).Value.Length; p++)
            {
                //for every ingredient needed for said item
                if(Instance.recipeUnlocked.ElementAt(p).Value == false)
                {
                    //if this has not been unlocked yet...   do math with 1 and 0

                    Debug.Log("");
                    if( canCraft 
                        && (numItemsTotal.ContainsKey(Instance.recipeRequirements.ElementAt(i).Value.ElementAt(p).id)
                        && (numItemsTotal[Instance.recipeRequirements.ElementAt(i).Value.ElementAt(p).id] >= Instance.recipeRequirements.ElementAt(i).Value.ElementAt(p).amount)
                        ))
                    {
                        //Debug.Log("you have enough for: " + Instance.recipeRequirements.ElementAt(i).Value.ElementAt(p).id + " This much: " + numItemsTotal[Instance.recipeRequirements.ElementAt(i).Value.ElementAt(p).id]);
                    }else
                    {
                        canCraft = false;
                    }
                    //Debug.Log(recipeRequirements);
                }
                //Debug.Log(Instance.recipeRequirements.ElementAt(i).Value[p].id);
            }
            Debug.Log("CAN CRAFT STATUS: " + canCraft + " " + Instance.recipeRequirements.ElementAt(i).Key);
            

        }
    }
    //// Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            UpdateInventory();
            //CheckUnlocks();
            Debug.Log("Can craft acorn? " + CanCraftItem("woodenAxe"));
            Debug.Log("Can craft stick? " + CanCraftItem("stick"));
            ShowPopUp("acorn");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateInventory();
            //CheckUnlocks();
            Debug.Log("Can craft currentItem? " + CanCraftItem(CurrentItem));
            CanCraftToggle(CanCraftItem(CurrentItem));
            //ShowPopUp("acorn");
        }

    }

    void InitializeCategoryBoxes()
    {
        for(int i = 0; i < CategoryBoxes.Length; i++)
        {
            ItemCategory iC = CategoryBoxes[i].GetComponent<ItemCategory>();
            if(CategoryArrayName.ElementAtOrDefault(i) != null)
            {
                iC.CategoryName = CategoryArrayName[i];
                iC.GenerateImage();
                if (i == 0)
                {
                    InitializeItemsToCraft(iC.CategoryName);
                }
            }
            else
            {
                iC.HideSprite();
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
                if(CraftingProperties.categoyItems[categoryName].ElementAtOrDefault(i) != null)
                {
                    iC.ItemName = CraftingProperties.categoyItems[categoryName][i];
                    iC.GenerateImage();
                    //to be removed
                    if (i == 0)
                    {
                        CurrentItem = iC.ItemName;
                    }
                }
                else
                {
                    iC.HideSprite();
                }

            }
            catch
            {
                Debug.Log(new System.Exception("No category available for this name"));
            }
        }
    }
    void InitializeIngredients()
    {
        for (int i = 0; i < IngredientBoxes.Length; i++)
        {
            IngredientBox iC = IngredientBoxes[i].GetComponent<IngredientBox>();
            iC.ClearQuantity();
            if (Instance.recipeRequirements.ContainsKey(CurrentItem) && Instance.recipeRequirements[CurrentItem].ElementAtOrDefault(i) != null)
            {
                iC.name = Instance.recipeRequirements[CurrentItem][i].id;
                iC.GenerateImage(iC.name);
                iC.GenerateQuantity(Instance.recipeRequirements[CurrentItem][i].amount);
            }
            else
            {
                iC.HideSprite();
            }
        }
    }

    public void UpdateItem(int index)
    {
        Instance.CurrentItem = Instance.ItemToCraftBoxes[index].GetComponent<ItemToCraft>().ItemName;
        Instance.SetItemInfo(Instance.CurrentItem);
        Instance.InitializeIngredients();
    }

    void SetItemInfo(string CurrentItemName)
    {
        Instance.ItemDesc.GetComponent<TextMeshProUGUI>().text = ItemProperties.itemDescription[CurrentItemName];
        Instance.ItemName.GetComponent<TextMeshProUGUI>().text = CurrentItemName;
        Instance.ItemToCraft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Items/" + CurrentItemName);
        Instance.CraftingAmount.GetComponent<TextMeshProUGUI>().text = "x " + GetCraftingItemAmount();
    }

    int GetCraftingItemAmount()
    {
        if(CraftingProperties.craftingItemAmount.ContainsKey(CurrentItem))
        {
            return CraftingProperties.craftingItemAmount[CurrentItem];
        }
        return 1;
    }

    void ShowPopUp(string itemName)
    {
        GameObject popUp = Instantiate(Resources.Load("Prefabs/ItemNotification")) as GameObject;
        ItemAddNotification itemNotification = popUp.GetComponentInChildren<ItemAddNotification>();
        itemNotification.AddItem(itemName);
    }
    private void CanCraftToggle(bool canCraft)
    {
        CanCraft.SetActive(!canCraft);
    }
}
