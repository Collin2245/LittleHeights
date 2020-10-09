using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool mouseOnTree;
    int treeCounter;
    PlayerInventory playerInventory;
    public Item currentItem;
    string possibleAxe;

    bool tempHoe;
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        treeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveItem();
        TryChopTree();
        Debug.Log(treeCounter);
        TryDestroyTree();
    }


    void OnMouseOver()
    {
        // Debug.Log("Tree has been held over");
        mouseOnTree = true;
    }

    void OnMouseExit()
    {
        // Debug.Log("Mouse has left tree");
        mouseOnTree = false;
    }


    void CheckActiveItem()
    {
        if (playerInventory.currentItem)
        {
            currentItem = playerInventory.currentItem;
            possibleAxe = currentItem.id;

        }
        else
        {
            currentItem = null;
        }
    }

    void TryChopTree()
    {
        if (mouseOnTree && Input.GetMouseButton(0) && currentItem)
        {
            switch (possibleAxe)
            {
                case "tempHoe":
                    treeCounter += 1;
                    Debug.Log("Chopping with temp hoe");
                    break;
                default:
                    Debug.Log("Not an axe");
                    break;
            }
        }
        else
        {
            treeCounter = 0;
        }
    }

    void TryDestroyTree()
    {
        if(treeCounter >= 100)
        {
            //Instantiate()
            Destroy(transform.gameObject);
        }
    }
}
