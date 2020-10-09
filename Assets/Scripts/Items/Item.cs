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
    public Sprite sprite;
    private string path;
    public RectTransform rectTransform;
    public Image image;
    public int maxQuantity;
    //public int currQuantity;
    public int currAmount;
    GameObject itemCountPrefab;
    public float itemCountOffsetX;
    public float itemCountOffsetY;
    private bool hasImage;

    


    void Start()
    {
        hasImage = false;
        path = "Items/" + this.id;
        sprite = Resources.Load<Sprite>(path);
        rectTransform = GetComponent<RectTransform>();
        if(this.TryGetComponent(out Image image))
        {
            hasImage = true;
            image = GetComponent<Image>();
            image.sprite = this.sprite;
        }
        
        if (this.id != "")
        {
            maxQuantity = ItemQuantities.quantityForItem[id];
        }

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
        if(hasImage)
        {
            image.sprite = this.sprite;
        }
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
            itemCountPrefab.GetComponentInChildren<TextMeshProUGUI>().SetText(currAmount.ToString());
            
            //itemCountPrefab.GetComponentInChildren<TextMeshProUGUI>().GetComponent<TextContainer>().width = 20;

        }
        itemCountPrefab.GetComponentInChildren<TextMeshProUGUI>().SetText(currAmount.ToString());

    }

    void addQuantity(int ammount)
    {

    }

}
