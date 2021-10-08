using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAddNotification : MonoBehaviour
{
    // Start is called before the first frame update

    Image image;
    void Start()
    {
        Destroy(transform.parent.parent.gameObject, 4f);
        image = this.GetComponent<Image>();
        this.GetComponent<Animator>().Play("PopUp",0);
    }

    // Update is called once per frame


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
