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
    void FixedUpdate()
    {
        CheckActiveItem();
        TryChopTree();
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
                    Debug.Log(treeCounter);
                    break;
                default:
                    Debug.Log("Not an axe");
                    break;
            }
        }
        else
        {
            treeCounter = 0;
            Debug.Log("No item in hand");
        }
    }

    void TryDestroyTree()
    {
        if(treeCounter >= 100)
        {
            GameObject wood = Instantiate(Resources.Load("Prefabs/DroppedItemPrefab"), new Vector3(transform.position.x + Random.Range(-3f,3f), transform.position.y + Random.Range(-3f,3f), transform.position.z), Quaternion.identity) as GameObject;
            wood.GetComponent<Item>().id = "tempHoe";
            wood.GetComponent<Item>().currAmount = Random.Range(1,5);
            Destroy(transform.gameObject);
        }
    }
}
