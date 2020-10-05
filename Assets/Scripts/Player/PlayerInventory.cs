using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currentItemHolder;
    public string currentItemId;
    //GameObject hotbar;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentItemHolder = GameObject.Find("CurrentItemSelector").GetComponent<currentItem>().currentSlot;
        if(currentItemHolder.transform.childCount > 0)
        {
            currentItemId = currentItemHolder.GetComponentInChildren<Item>().id;
        }
        else
        {
            currentItemId = "";
        }
        
    }
}
