using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAddNotification : MonoBehaviour
{
    // Start is called before the first frame update
    Transform itemAddtransform;
    int timer;
    void Start()
    {
        itemAddtransform = this.GetComponent<Transform>();
        itemAddtransform.position = new Vector3(956.067078f, 534, -56.8727722f);
        timer = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= 1;
        }else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string ItemName)
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image i in images)
        {
            if(i.name == "NewItemImage")
            {
                i.sprite = Resources.Load<Sprite>("Items/" + ItemName);
            }
        }
        
    }
}
