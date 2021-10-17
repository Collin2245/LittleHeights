using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCraftingItem : MonoBehaviour
{

    public List<GameObject> holders;
    public int currentSlotNum;
    public int currentArrayNum;
    Canvas mainUi;
    private void Start()
    {
        mainUi = GameObject.Find("MainUI").GetComponent<Canvas>();
        currentSlotNum = 0;
        currentArrayNum = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(mainUi.isActiveAndEnabled)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                scrollUp();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                scrollDown();
            }
            this.transform.position = new Vector3(holders[currentSlotNum].transform.position.x, holders[currentSlotNum].transform.position.y, holders[currentSlotNum].transform.position.z);
        }
    }
    private void scrollDown()
    {
        if (currentSlotNum < holders.Count -1)
        {
            if(currentSlotNum < CraftingTab.Instance.itemsInCategory -1)
            {
                currentSlotNum += 1;
                CraftingTab.Instance.UpdateItem(currentSlotNum);
            }
        }
        else if(currentArrayNum < CraftingTab.Instance.itemsInCategory - 1)
        {
            currentSlotNum = 3;
            currentArrayNum += 1;
            CraftingTab.Instance.ShiftItems(currentArrayNum);
            CraftingTab.Instance.UpdateItem(currentSlotNum);
        }
        //CraftingTab.Instance.UpdateItem(currentSlotNum);
    }

    private void scrollUp()
    {
        if (currentSlotNum > 0)
        {
            currentSlotNum -= 1;
            CraftingTab.Instance.UpdateItem(currentSlotNum);
        }
        else if (currentArrayNum > 3)
        {
            currentSlotNum = 0;
            currentArrayNum -= 1;
            CraftingTab.Instance.ShiftItems(currentArrayNum);
            CraftingTab.Instance.UpdateItem(currentSlotNum);
        }
    }
    public void UpdatePosition(int pos)
    {
        currentSlotNum = pos;
        CraftingTab.Instance.UpdateItem(pos);
    }

    public void UpdateItemTotalPosition(int pos)
    {
        currentArrayNum = pos;
        //CraftingTab.Instance.UpdateItem(pos);
    }
}
