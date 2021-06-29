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
                MouseInventorySlot.Instance.itemPrefabOnMouse.transform.position = new Vector3(Input.mousePosition.x + 100, Input.mousePosition.y - 100, Input.mousePosition.z);
            }

        }
        generateInventory();
    }

    public void generateInventory()
    {
        if (itemId != "")
        {
            if (this.transform.childCount == 0)
            {
                itemPrefab = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, rectTransform);
                item = itemPrefab.GetComponent<Item>();
                itemId = item.id;
                currAmount = item.currAmount;
            }
            else
            {
                path = "Items/" + this.itemId;
                transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(path);
                item = GetComponentInChildren<Item>();
                itemId = item.id;
                currAmount = item.currAmount;
            }
        }
        else
        {
            if (this.transform.childCount >= 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
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
                currAmount = MouseInventorySlot.Instance.currAmount;
                itemPrefab = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, rectTransform);
                item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
                item.currAmount = currAmount;
                DestroyMousePrefab();
            }
            else
            {
                if(itemId == MouseInventorySlot.Instance.itemIdOnMouse && currAmount + MouseInventorySlot.Instance.currAmount <= ItemProperties.quantityForItem[itemId])
                {
                    currAmount = currAmount + MouseInventorySlot.Instance.currAmount;
                    DestroyMousePrefab();
                    item.currAmount = currAmount;
                }
                else
                {
                    item = this.GetComponentInChildren<Item>();
                    string tempItemId = item.id;
                    int tempQuantity = item.currAmount;
                    item.id = MouseInventorySlot.Instance.itemIdOnMouse;
                    item.currAmount = MouseInventorySlot.Instance.currAmount;
                    Item tempItem = MouseInventorySlot.Instance.GetComponentInChildren<Item>();
                    tempItem.id = tempItemId;
                    tempItem.currAmount = tempQuantity;
                    MouseInventorySlot.Instance.itemIdOnMouse = tempItemId;
                    MouseInventorySlot.Instance.currAmount = tempQuantity;
                }
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
                //make this another function
                MouseInventorySlot.Instance.itemIdOnMouse = itemId;
                MouseInventorySlot.Instance.currAmount = currAmount;
                MouseInventorySlot.Instance.itemOnMouse = true;
                itemId = "";
                item.id = "";   //something weird here
                MouseInventorySlot.Instance.itemPrefabOnMouse = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, MouseInventorySlot.Instance.transform);
                Item tempItem = MouseInventorySlot.Instance.GetComponentInChildren<Item>();
                tempItem.id = itemId;
                tempItem.currAmount = currAmount;
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        generateInventory();
    }

    void DestroyMousePrefab()
    {
        Destroy(MouseInventorySlot.Instance.itemPrefabOnMouse);
        MouseInventorySlot.Instance.itemIdOnMouse = "";
        MouseInventorySlot.Instance.itemOnMouse = false;
        MouseInventorySlot.Instance.currAmount = 0;
        foreach (Transform child in MouseInventorySlot.Instance.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
