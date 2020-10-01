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
    private RectTransform rectTransform;
    private Button btn;
    private Item item;
    private Item item2;
    public string itemMovement;
    public GameObject mouseInventory;
    void Start()
    {
        mouseInventory = GameObject.Find("MouseInventorySlot");
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => InventoryButtonClicked());
        itemPrefab = Resources.Load("Prefabs/ItemPrefab") as GameObject;
        rectTransform = GetComponent<RectTransform>();
        generateInventory();
    }

    // Update is called once per frame
    void Update()
    {

        // check if itemOnMouse is active, if it is, have item instance follow mouse until button is next clicked
        if (MouseInventorySlot.Instance.itemOnMouse) //item follow mouse
        {
            Debug.Log("mouse tracking is active");
            if(this.transform.childCount == 0)
            {
                Debug.Log("Generated the item");
                //itemPrefab. .transform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
                itemPrefab = Instantiate(itemPrefab, new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z), Quaternion.identity);
                item2 = itemPrefab.GetComponent<Item>();
                item2.id = itemId;
            }
            else
            {
                Debug.Log("Following the item");
                itemPrefab.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }

        }
    }

    void generateInventory()
    {
        if (itemId != "")
        {
            //item is attached
            if (this.transform.childCount == 0)
            {
                //item has not been drawn yet

                itemPrefab = Instantiate(itemPrefab, rectTransform);
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
                MouseInventorySlot.Instance.itemOnMouse = false;
                MouseInventorySlot.Instance.itemIdOnMouse = "";
                //item swapped and no item on mouse
                
            }
            //add if not empty
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
                Destroy(this.transform.GetChild(0).gameObject);



            }
        }
        generateInventory();
    }
}
