using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn;
    public Image currentPage;
    public Image destinationPage;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => CraftingButtonClicked());
        //hotbarGameObject = GameObject.Find("HotbarCanvas");
        //inventoryGameObject = GameObject.Find("InventoryCanvas");
        //craftingGameObject = GameObject.Find("CraftingCanvas");
        //inventoryCanvas = inventoryGameObject.GetComponent<Canvas>();
        //hotbarCanvas = hotbarGameObject.GetComponent<Canvas>();
        //craftingCanvas = craftingGameObject.GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CraftingButtonClicked()
    {
        Debug.Log("Clicked!");
        if (currentPage.enabled == true)
        {
            EnableAllChildren();
            DisableAllChildren();
        }
        else
        {
            Debug.LogError("Already on page");
        }
    }

    void EnableAllChildren()
    {
        foreach (Image r in destinationPage.GetComponentsInChildren(typeof(Image)))
        {
            r.enabled = true;
        }
        destinationPage.enabled = true;
    }

    void DisableAllChildren()
    {
        foreach (Image r in currentPage.GetComponentsInChildren(typeof(Image)))
        {
            r.enabled = false;
        }
        currentPage.enabled = false;
    }
}
