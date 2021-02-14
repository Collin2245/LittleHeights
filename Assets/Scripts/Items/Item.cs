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
            maxQuantity = ItemProperties.quantityForItem[id];
        }

    }


    // Update is called once per frame
    void Update()
    {
        //rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        path = "Items/" + this.id;
        sprite = Resources.Load<Sprite>(path);
        if(hasImage)
        {
            image.sprite = this.sprite;
        }
        if(this.id != "")
        {
            maxQuantity = ItemProperties.quantityForItem[id];
        }
        if(this.transform.childCount == 0)
        {
            itemCountPrefab = Instantiate(Resources.Load("Prefabs/ItemCountPrefab"), this.transform) as GameObject;
            itemCountPrefab.GetComponentInChildren<TextMeshProUGUI>().SetText(currAmount.ToString(),true);
        }
        itemCountPrefab.GetComponentInChildren<TextMeshProUGUI>().SetText(currAmount.ToString());

    }

    void addQuantity(int ammount)
    {
    }

    public void subtractQuantity(int amount)
    {
        this.currAmount -= amount;
        if(this.currAmount == 0)
        {
            this.id = "";
        }
    }

}
