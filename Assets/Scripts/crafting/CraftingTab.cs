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
    public GameObject CraftButtonObj;
    Button button;
    public GameObject ItemName;
    public GameObject CraftingAmount;
    public GameObject CanCraft;
    //public GameObject Player
    public GameObject CategorySelector;
    AudioSource audioSource;
    public string[] CategoryArrayName;
    public string CurrentItem;
    public GameObject junk;
    public int itemsInCategory;
    int itemNumberForCurrItems;
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
        Instance.audioSource = GetComponent<AudioSource>();
        Instance.recipeUnlocked = CraftingProperties.RecipeUnlocked;
        button = CraftButtonObj.GetComponent<Button>();
        Instance.junk.transform.localScale = new Vector3(0f, 0f);
        InitializeCategoryBoxes(0);
        SetItemInfo(CurrentItem);
        InitializeIngredients();
        button.onClick.AddListener(TryCrafting);
    }

    void TryCrafting()
    {
        UpdateInventory();
        bool canCraft = CanCraftItem(CurrentItem);
        CanCraftToggle(canCraft);
        if(canCraft)
        {
            audioSource.pitch = Random.Range(0.7f, 1.5f);
            audioSource.PlayOneShot(audioSource.clip);
            CraftItem(CurrentItem);
        }
        UpdateInventory();
        canCraft = CanCraftItem(CurrentItem);
        CanCraftToggle(canCraft);
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

    public void CraftItem(string itemId)
    {
        if(!Instance.recipeRequirements.ContainsKey(itemId))
        {
            Debug.LogError("No crafting info found for: " + itemId);
            return;
        }

        for(int i = 0; i < Instance.recipeRequirements[itemId].Length; i++)
        {
            if(TryRemoveFromInventory(Instance.recipeRequirements[itemId].ElementAt(i).id, Instance.recipeRequirements[itemId].ElementAt(i).amount))
            {
                continue;
            }else
            {
                Debug.LogError("Error happened removing item from inventory, able to craft when you should not be able to.");
                return;
            }
        }
        Item item = junk.AddComponent<Item>();
        item.id = itemId;
        item.currAmount = GetCraftingItemAmount();
        foreach (var comp in junk.GetComponents<Component>())
        {
            if (!(comp is Transform))
            {
                Destroy(comp);
            }
        }
        this.transform.parent.GetComponentInChildren<PlayerInventory>().TryToAddItemToInventoryNonDroppedItem(item);
    }
    public bool TryRemoveFromInventory(string itemId, int itemAmount)
    {

        if((! numItemsTotal.ContainsKey(itemId)) || numItemsTotal[itemId] < itemAmount)
        {
            return false;
        }

        itemHolders = GameObject.Find("Player").GetComponent<PlayerInventory>().itemHolders;
        foreach (GameObject item in itemHolders)
        {
            InventorySlot inventorySlot = item.GetComponent<InventorySlot>();
            if (inventorySlot.itemId == itemId)
            {
                if(inventorySlot.currAmount >= itemAmount)
                {
                    inventorySlot.GetComponentInChildren<Item>().subtractQuantity(itemAmount);
                    inventorySlot.generateInventory();
                    return true;
                }else
                {
                    int itemOnSlot = inventorySlot.GetComponentInChildren<Item>().currAmount;
                    inventorySlot.GetComponentInChildren<Item>().subtractQuantity(inventorySlot.GetComponentInChildren<Item>().currAmount);
                    inventorySlot.generateInventory();
                    itemAmount -= itemOnSlot;
                }
            }
        }
        return false;
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
                }
            }
            Debug.Log("CAN CRAFT STATUS: " + canCraft + " " + Instance.recipeRequirements.ElementAt(i).Key);
            

        }
    }
    //// Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    UpdateInventory();
        //    //CheckUnlocks();
        //    Debug.Log("Can craft acorn? " + CanCraftItem("woodenAxe"));
        //    Debug.Log("Can craft stick? " + CanCraftItem("stick"));
        //    ShowPopUp("acorn");
        //    TryRemoveFromInventory("acorn", 3);
        //}
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateInventory();
            //CheckUnlocks();
            Debug.Log("Can craft currentItem? " + CanCraftItem(CurrentItem));
            CanCraftToggle(CanCraftItem(CurrentItem));
            //ShowPopUp("acorn");
        }

    }

    public void InitializeCategoryBoxes(int categorySlot)
    {
        for (int i = 0; i < CategoryBoxes.Length; i++)
        {
            ItemCategory iC = CategoryBoxes[i].GetComponent<ItemCategory>();
            if (CategoryArrayName.ElementAtOrDefault(i) != null)
            {
                iC.CategoryName = CategoryArrayName[i];
                iC.GenerateImage();
                if (i == categorySlot)
                {
                    InitializeItemsToCraft(iC.CategoryName);
                    SetItemInfo(Instance.CurrentItem);
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
        itemsInCategory = CraftingProperties.categoyItems[categoryName].Count;
        Debug.Log("Amount of items in caregory " + categoryName + ": " + itemsInCategory);
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
                        CurrentCraftingItem currentCraftingItem = GameObject.Find("CurrentItemToCraft").GetComponent<CurrentCraftingItem>();
                        currentCraftingItem.UpdatePosition(0);
                        currentCraftingItem.UpdateItemTotalPosition(ItemToCraftBoxes.Length - 1);
                    }
                }
                else
                {
                    iC.ItemName = "";
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
        CanCraftToggle(CanCraftItem(CurrentItem));
    }

    int GetCraftingItemAmount()
    {
        if(CraftingProperties.craftingItemAmount.ContainsKey(CurrentItem))
        {
            return CraftingProperties.craftingItemAmount[CurrentItem];
        }
        return 1;
    }


    public void ShiftItems(int offSet)
    {
        int counter = 0;
        string categoryName = CategorySelector.GetComponent<CurrentCategorySelector>().GetCurrentCategoryName();
        itemsInCategory = CraftingProperties.categoyItems[categoryName].Count;
        //Debug.Log("Amount of items in caregory " + categoryName + ": " + itemsInCategory);
        int startingPos = offSet - (ItemToCraftBoxes.Length - 1);
        // add a check to end of for loop if 4 is more then the count of item categories
        for (int i = startingPos; i < (startingPos + (ItemToCraftBoxes.Length)); i++)
        {

            try
            {
                ItemToCraft iC = ItemToCraftBoxes[counter].GetComponent<ItemToCraft>();
                if (CraftingProperties.categoyItems[categoryName].ElementAtOrDefault(i) != null)
                {
                    iC.ItemName = CraftingProperties.categoyItems[categoryName][i];
                    iC.GenerateImage();
                    //to be removed
                    if (i == 0)
                    {
                        CurrentItem = iC.ItemName;
                        CurrentCraftingItem currentCraftingItem = GameObject.Find("CurrentItemToCraft").GetComponent<CurrentCraftingItem>();
                        currentCraftingItem.UpdatePosition(0);
                        currentCraftingItem.UpdateItemTotalPosition(ItemToCraftBoxes.Length - 1);
                    }
                }
                else
                {
                    iC.HideSprite();
                }
                counter++;
            }
            catch
            {
                Debug.Log(new System.Exception("No category available for this name"));
            }
        }
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
