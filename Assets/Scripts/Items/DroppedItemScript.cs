using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public class DroppedItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    Item item;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        item = this.GetComponent<Item>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
        spriteRenderer.sprite = item.sprite;

    }

    // Update is called once per frame
    void Update()
    {
        //spriteRenderer.sprite = item.sprite;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && !collider.isTrigger)
        {
            //Debug.Log("Hit player tag");
            collider.gameObject.GetComponent<PlayerInventory>().TryToAddItemToInventory(item);
            //CraftingTab.Instance.UpdateInventory();
        }
    }
}
