using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public string id;
    public string type;
    private Sprite sprite;
    private string path;
    public RectTransform rectTransform;
    public Image image;
    public int maxQuantity;
    //public int currQuantity;
    public int currQuantity;
    GameObject itemCountPrefab;
    public float itemCountOffsetX;
    public float itemCountOffsetY;

    


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        path = "Items/" + this.id;
        sprite = Resources.Load<Sprite>(path);
        image.sprite = this.sprite;
        if (this.id != "")
        {
            maxQuantity = ItemQuantities.quantityForItem[id];
        }
        itemCountOffsetX = -70;
        itemCountOffsetY = 70;

    }


    // Update is called once per frame
    void Update()
    {
        //if(this.currQuantity == 0)
        //{
        //    this.currQuantity = 1;
        //}
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        path = "Items/" + this.id;
        sprite = Resources.Load<Sprite>(path);
        image.sprite = this.sprite;
        if(this.id != "")
        {
            maxQuantity = ItemQuantities.quantityForItem[id];
        }
        if(this.transform.childCount == 0)
        {
            itemCountPrefab = Instantiate(Resources.Load("Prefabs/ItemCountPrefab"), this.transform) as GameObject;
            itemCountPrefab.transform.position = new Vector3(this.transform.position.x - itemCountOffsetX, this.transform.position.y - itemCountOffsetY, this.transform.position.z);
            //if(this.currQuantity.ToString() == "")
            //{
            //    Debug.Log("Oberwriting text");
            //    this.currQuantity = 1;
            //}
            itemCountPrefab.GetComponentInChildren<TextMeshProUGUI>().SetText(currQuantity.ToString());
            
            //itemCountPrefab.GetComponentInChildren<TextMeshProUGUI>().GetComponent<TextContainer>().width = 20;

        }



        //this is for checking the quantity of the itme, I think it should only be ran when checking quantity of item -- refer to later in pickup
        //if(id != null)
        //{
        //    if (ItemQuantities.quantityForItem.TryGetValue(this.id, out int result))
        //    {
        //        Debug.Log(result);
        //    }
        //}

    }

}
