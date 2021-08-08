using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    //// Start is called before the first frame update
    //bool mouseOnTree;
    int treeCounter;
    PlayerInventory playerInventory;
    //public Item currentItem;
    string possibleAxe;
    GameObject tileManager;
    bool playAudio = false;
    //bool inLastCheck;
    //Camera mainCamera;
    //int treeMask;

    //bool tempHoe;
    //private void Awake()
    //{
    //    treeMask = LayerMask.GetMask("Tree");

    //}
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        treeCounter = 0;
        playAudio = true;
        tileManager = GameObject.FindGameObjectWithTag("TileManager");
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    //CheckActiveItem();
    //    //TryChopTree();
    //    TryDestroyTree();
    //}

    //bool CheckIfMouseOnTree()
    //{

    //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1 , treeMask);
    //    if (hit.collider != null && hit.transform.name == this.name)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}    
    //void CheckActiveItem()
    //{
    //    if (playerInventory.currentItem)
    //    {
    //        currentItem = playerInventory.currentItem;
    //        possibleAxe = currentItem.id;
    //    }
    //    else
    //    {
    //        currentItem = null;
    //    }
    //}
    public void resetCounter()
    {
        treeCounter = 0;
        playAudio = true;

    }

    public void TryChopTree(Vector3Int point, TileManager tileManager)
    {
        if (treeCounter >= 100)
        {
            int randomNumWoord = Random.Range(1, 6);
            for (int i = 0; i < randomNumWoord; i++)
            {
                GameObject wood = Resources.Load("Prefabs/DroppedItemPrefab") as GameObject;
                wood.GetComponent<Item>().id = "wood";
                wood.GetComponent<Item>().currAmount = Random.Range(1, 4);
                Instantiate(wood, new Vector3(point.x + Random.Range(-3f, 3f), point.y + Random.Range(-3f, 3f), point.z), Quaternion.identity);

            }
            int randomNumAcorn = Random.Range(1, 3);
            for (int i = 0; i < randomNumAcorn; i++)
            {
                GameObject acorn = Resources.Load("Prefabs/DroppedItemPrefab") as GameObject;
                acorn.GetComponent<Item>().id = "acorn";
                acorn.GetComponent<Item>().currAmount = 1;
                Instantiate(acorn, new Vector3(point.x + Random.Range(-3f, 3f), point.y + Random.Range(-3f, 3f), point.z), Quaternion.identity);
            }
            treeCounter = 0;
            tileManager.GetComponent<TileManager>().extraMapCollide.SetTile(point, null);
            tileManager.GetComponent<TileManager>().GetTileInfoAtPoint(point).isTreeOn = false;
        }
        else
        {
            if (tileManager.GetComponent<MouseHoverScript>().isActiveArea)
            {
                switch (possibleAxe)
                {
                    case "woodenAxe":
                        treeCounter += 2;
                        if (playAudio)
                        {
                            this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
                            playAudio = false;
                        }else if (this.GetComponent<AudioSource>().isPlaying == false)
                        {
                            StartCoroutine(Wait());
                        }
                        Debug.Log(treeCounter);
                        break;
                    default:
                        Debug.Log("Not an axe");
                        if (playAudio)
                        {
                            this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
                            playAudio = false;
                        }
                        else if (this.GetComponent<AudioSource>().isPlaying == false)
                        {
                            StartCoroutine(Wait());
                        }
                        treeCounter += 1;
                        break;
                }
            }
            else
            {
                this.GetComponent<AudioSource>().Stop();
                playAudio = false;
                treeCounter = 0;
            }
        }

    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        playAudio = true;
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