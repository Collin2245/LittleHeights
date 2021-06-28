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
    Vector3 offset;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => CraftingButtonClicked());
        offset = new Vector3(9999999f, 9999999f, 999999f);
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
            ShowAllChildren();
            HideAllChildren();
    }

    void ShowAllChildren()
    {
        Transform destTransform = destinationPage.transform;
        if(destTransform.position.x > 99999f)
        {
            destTransform.position -= offset;
        }
    }

    void HideAllChildren()
    {
        Transform currTransform = currentPage.transform;
        currTransform.position += offset;
    }

}
