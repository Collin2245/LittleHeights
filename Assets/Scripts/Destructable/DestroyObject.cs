using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //// Start is called before the first frame update
    int itemCounter;
    PlayerInventory playerInventory;
    string possibleItem;
    GameObject tileManager;
    bool playAudio = false;
    public AudioClip treeClip;
    AudioSource audioSource;
    IEnumerator treeEnumerator;
    Coroutine treeCoroutine;

    void Start()
    {
        treeEnumerator = loopAudio("tree", 0.5f);
        StopCoroutine(treeCoroutine);
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        itemCounter = 0;
        tileManager = GameObject.FindGameObjectWithTag("TileManager");
        audioSource = GetComponent<AudioSource>();
        possibleItem = "";
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();


    }

    public void resetCounter()
    {
        itemCounter = 0;
        playAudio = false;

    }

    IEnumerator loopAudio(string sourceName, float timeBetweenAudio)
    {
        playAudio = true;
        switch (sourceName)
        {
            case "tree":
                audioSource.clip = treeClip;
                break;
        }    

        while(playAudio)
        {
            Debug.Log("In corutine" + itemCounter);
            audioSource.PlayOneShot(audioSource.clip);
            yield return new WaitForSeconds(timeBetweenAudio);
        }
        audioSource.Stop();
    }

    public void TryDestroyObject(Vector3Int point, TileManager tileManager, string objectToDestroy)
    {
        switch (objectToDestroy)
        {
            case "tree":
                destroyTree(point);
                break;
            default:
                Debug.Log("Not destructable object");
                break;
        }
    }

    void destroyTree(Vector3Int point)
    {
        if (itemCounter >= 100)
        {
            playAudio = false;
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
            itemCounter = 0;
            StopCoroutine(treeCoroutine);
            tileManager.GetComponent<TileManager>().extraMapCollide.SetTile(point, null);
            tileManager.GetComponent<TileManager>().GetTileInfoAtPoint(point).isTreeOn = false;
        }
        else
        {
            if (tileManager.GetComponent<MouseHoverScript>().isActiveArea)
            {
                if (!playAudio)
                    StartCoroutine(treeEnumerator);
                SetCurrentItem();
                switch (possibleItem)
                {
                    case "woodenAxe":
                        itemCounter += 2;
                        Debug.Log(itemCounter);
                        break;
                    default:
                        Debug.Log("Not an axe");
                        itemCounter += 1;
                        break;
                }
            }
            else
            {
                playAudio = false;
                itemCounter = 0;
                StopCoroutine(treeEnumerator);
            }
        }
    }

    private void SetCurrentItem()
    {
        if (playerInventory.currentItem != null)
        {
            possibleItem = playerInventory.currentItem.id;
        }
        else
        {
            possibleItem = "";
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