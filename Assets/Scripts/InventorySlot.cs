using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public string itemId;
    private bool itemHasBeenDrawn;
    public GameObject itemPrefab;
    //public GameObject MouseInventorySlot.Instance.itemPrefabOnMouse;
    private RectTransform rectTransform;
    private Button btn;
    public Item item;
    public string itemMovement;
    public GameObject mouseInventory;
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
            //Debug.Log("mouse tracking is active");
            if (MouseInventorySlot.Instance.transform.childCount == 0)
            {
                //Debug.Log("Generated the item");
                //itemPrefab. .transform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
                MouseInventorySlot.Instance.itemPrefabOnMouse = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, MouseInventorySlot.Instance.transform);
                MouseInventorySlot.Instance.itemPrefabOnMouse.GetComponent<Item>().id = MouseInventorySlot.Instance.itemIdOnMouse;
            }
            else
            {
                //Debug.Log("Following the item");
                MouseInventorySlot.Instance.itemPrefabOnMouse.transform.position = new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 80, Input.mousePosition.z);
            }

        }
        // check if itemOnMouse is active, if it is, have item instance follow mouse until button is next clicked

        else
        {
            //maybe do something here
            //foreach (Transform child in MouseInventorySlot.Instance.transform)
            //{
            //    Destroy(child.gameObject);
            //}
        }
        generateInventory();
    }

    void generateInventory()
    {
        if (itemId != "")
        {
            //item is attached
            if (this.transform.childCount == 0)
            {
                //item has not been drawn yet

                itemPrefab = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, rectTransform);
                item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
            }
            //else
            //{
                
            //}
        }
    }

    void InventoryButtonClicked()
    {
        if(MouseInventorySlot.Instance.itemOnMouse)//clicked when item is on mouse
        {
            if(itemId == "")
            {
                itemId = MouseInventorySlot.Instance.itemIdOnMouse;
                itemPrefab = Instantiate(itemPrefab, rectTransform);
                item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
                //item swapped and no item on mouse
                Destroy(MouseInventorySlot.Instance.itemPrefabOnMouse);
                MouseInventorySlot.Instance.itemIdOnMouse = "";
                MouseInventorySlot.Instance.itemOnMouse = false;
                foreach (Transform child in MouseInventorySlot.Instance.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            //add if not empty
            else
            {
                string tempItem = itemId;
                itemId = MouseInventorySlot.Instance.itemIdOnMouse;
                item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
                MouseInventorySlot.Instance.itemIdOnMouse = tempItem;
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
                Debug.Log("Clicked item with no item on mouse and valid item in slot");
                //make this another function
                MouseInventorySlot.Instance.itemIdOnMouse = itemId;
                MouseInventorySlot.Instance.itemOnMouse = true;
                itemId = "";
                item.id = "";
                MouseInventorySlot.Instance.itemPrefabOnMouse = Instantiate(Resources.Load("Prefabs/ItemPrefab") as GameObject, MouseInventorySlot.Instance.transform);
                foreach (Transform child in rectTransform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        generateInventory();
    }
}
