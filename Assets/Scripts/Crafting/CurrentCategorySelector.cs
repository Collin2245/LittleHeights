using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCategorySelector : MonoBehaviour
{
    public List<GameObject> holders;
    int currentSlotNum;
    private void Start()
    {
        currentSlotNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(holders[currentSlotNum].transform.position.x, holders[currentSlotNum].transform.position.y, holders[currentSlotNum].transform.position.z);
    }
    
    public void UpdatePosition(int pos)
    {
        currentSlotNum = pos;
    }

    public string GetCurrentCategoryName()
    {
        return CraftingProperties.categoryNames[currentSlotNum];
    }
}
