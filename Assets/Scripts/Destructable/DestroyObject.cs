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
    public bool playAudio = false;
    public AudioClip treeClip;
    AudioSource audioSource;
    IEnumerator treeEnumerator;

    void Start()
    {
        playAudio = false;
        treeEnumerator = loopAudio("tree", 0.4f);
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

    public void stopAllCoroutinesPlease()
    {
        StopAllCoroutines();
    }

    IEnumerator loopAudio(string sourceName, float timeBetweenAudio)
    {
        float pitchFloor;
        float pitchCeiling;
        switch (sourceName)
        {
            case "tree":
                audioSource.clip = treeClip;
                pitchFloor = 0.3f;
                pitchCeiling = 2f;
                break;
            default:
                audioSource.clip = null;
                pitchFloor = 0f;
                pitchCeiling = 0f;
                break;
        }    

        while(playAudio)
        {
            audioSource.pitch = Random.Range(pitchFloor, pitchCeiling);
            audioSource.PlayOneShot(audioSource.clip);
            yield return new WaitForSeconds(timeBetweenAudio);
        }
        audioSource.Stop();
    }

    public void TryDestroyObject(Vector3Int point, string objectToDestroy)
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

    void SpawnItem(int amountToSpawn,string name, Vector3Int point)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject item = Resources.Load("Prefabs/DroppedItemPrefab") as GameObject;
            item.GetComponent<Item>().id = name;
            Instantiate(item, new Vector3(point.x + Random.Range(-4, 4f), point.y + Random.Range(-4f, 4f), point.z), Quaternion.identity);
        }
    }

    void destroyTree(Vector3Int point)
    {
        if (itemCounter >= 100)
        {
            playAudio = false;
            int randomNumWood = Random.Range(8, 12);
            SpawnItem(randomNumWood, "wood", point);
            int randomNumAcorn = Random.Range(1, 3);
            SpawnItem(randomNumAcorn, "acorn", point);
            itemCounter = 0;
            StopCoroutine(treeEnumerator);
            tileManager.GetComponent<TileManager>().extraMapCollide.SetTile(point, null);
            tileManager.GetComponent<TileManager>().GetTileInfoAtPoint(point).isTreeOn = false;
        }
        else
        {
            if(playAudio == false)
            {
                playAudio = true;
                StartCoroutine(treeEnumerator);
            }
            playAudio = true;
            switch (possibleItem)
            {
                case "woodenAxe":
                    itemCounter += 2;
                    //Debug.Log(itemCounter);
                    break;
                default:
                    //Debug.Log("Not an axe");
                    itemCounter += 1;
                    break;
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