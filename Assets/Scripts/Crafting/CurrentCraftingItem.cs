using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCraftingItem : MonoBehaviour
{

    public List<GameObject> holders;
    public int currentSlotNum;
    private void Start()
    {
        currentSlotNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            scrollDown();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            scrollUp();
        }
        this.transform.position = new Vector3(holders[currentSlotNum].transform.position.x, holders[currentSlotNum].transform.position.y, holders[currentSlotNum].transform.position.z);
    }
    private void scrollUp()
    {
        if (currentSlotNum < holders.Count -1)
        {
            currentSlotNum += 1;
        }
        else
        {
            currentSlotNum = 0;
        }
        CraftingTab.Instance.UpdateItem(currentSlotNum);
    }

    private void scrollDown()
    {
        if (currentSlotNum > 0)
        {
            currentSlotNum -= 1;
        }
        else
        {
            currentSlotNum = holders.Count -1;
        }
        CraftingTab.Instance.UpdateItem(currentSlotNum);
    }
    public void UpdatePosition(int pos)
    {
        currentSlotNum = pos;
        CraftingTab.Instance.UpdateItem(pos);
    }
}
