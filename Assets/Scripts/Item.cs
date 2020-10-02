using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public int currQuantity;

    


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        path = "Items/" + this.id ;
        sprite = Resources.Load<Sprite>(path);
        image.sprite = this.sprite;

    }

   
    // Update is called once per frame
    void Update()
    {
        //this is for checking the quantity of the itme, I think it should only be ran when checking quantity of item -- refer to later in pickup
        //if(id != null)
        //{
        //    if (ItemQuantities.quantityForItem.TryGetValue(this.id, out int result))
        //    {
        //        Debug.Log(result);
        //    }
        //}

    }

    public void display(float x, float y, float z = 0)
    {
        Vector3 location = new Vector3(x, y, z);
        rectTransform.position = location;
    }
}
