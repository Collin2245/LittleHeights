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
    public Sprite sprite;
    private string path;
    public RectTransform rectTransform;
    public Image image;



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
        
    }

    public void display(float x, float y, float z = 0)
    {
        Vector3 location = new Vector3(x, y, z);
        rectTransform.position = location;
    }
}
