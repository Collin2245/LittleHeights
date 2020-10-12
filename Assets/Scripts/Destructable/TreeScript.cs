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
    GameObject tileManager;
    bool playAudio = false;

    bool tempHoe;
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        treeCounter = 0;
        tileManager = GameObject.FindGameObjectWithTag("TileManager");
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
        if (mouseOnTree && Input.GetMouseButton(0) && currentItem && tileManager.GetComponent<MouseHoverScript>().isActiveArea)
        {
            switch (possibleAxe)
            {
                case "tempHoe":
                    treeCounter += 1;
                    playAudio = true;
                    if(playAudio && this.GetComponent<AudioSource>().isPlaying == false)
                    {
                        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
                        new WaitForSeconds(this.GetComponent<AudioSource>().clip.length * 2);
                        playAudio = false;
                    }
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
            this.GetComponent<AudioSource>().Stop();
            playAudio = false;
            treeCounter = 0;
            Debug.Log("No item in hand");
        }
    }

    void TryDestroyTree()
    {
        if(treeCounter >= 100)
        {
           int randomNumWoord = Random.Range(1,6);
            for(int i = 0; i < randomNumWoord; i ++)
            {
            GameObject wood  = Resources.Load("Prefabs/DroppedItemPrefab") as GameObject;
            wood.GetComponent<Item>().id = "wood";
            wood.GetComponent<Item>().currAmount = Random.Range(1,4);
            Instantiate(wood, new Vector3(transform.position.x + Random.Range(-3f,3f), transform.position.y + Random.Range(-3f,3f), transform.position.z), Quaternion.identity);
 
            }
            int randomNumAcorn = Random.Range(1,3);
            for(int i = 0; i < randomNumAcorn; i ++)
            {
            GameObject acorn = Resources.Load("Prefabs/DroppedItemPrefab") as GameObject;
            acorn.GetComponent<Item>().id = "acorn";
            acorn.GetComponent<Item>().currAmount = 1;
            Instantiate(acorn, new Vector3(transform.position.x + Random.Range(-3f,3f), transform.position.y + Random.Range(-3f,3f), transform.position.z), Quaternion.identity);
            }
            Destroy(transform.gameObject);
        }
    }
}


        //    int randomNumWoord = Random.Range(1,5);
        //     for(int i = 0; i < randomNumWoord; i ++)
        //     {
        //     GameObject wood = Instantiate(Resources.Load("Prefabs/ItemPrefab"), new Vector3(transform.position.x + Random.Range(-3f,3f), transform.position.y + Random.Range(-3f,3f), transform.position.z), Quaternion.identity) as GameObject;
        //     wood.GetComponent<Item>().id = "wood";
        //     wood.GetComponent<Item>().currAmount = Random.Range(1,2);
        //     }
        //     int randomNumAcorn = Random.Range(1,3);
        //     for(int i = 0; i < randomNumAcorn; i ++)
        //     {
        //     GameObject wood = Instantiate(Resources.Load("Prefabs/ItemPrefab"), new Vector3(transform.position.x + Random.Range(-3f,3f), transform.position.y + Random.Range(-3f,3f), transform.position.z), Quaternion.identity) as GameObject;
        //     wood.GetComponent<Item>().id = "acorn";
        //     wood.GetComponent<Item>().currAmount = 1;
        //     }