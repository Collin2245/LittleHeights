using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public string itemId;
    private bool itemHasBeenDrawn;
    public GameObject itemPrefab;
    void Start()
    {
        itemPrefab = Resources.Load("Prefabs/ItemPrefab") as GameObject;
        if (itemId != null)
        {
            //item is attached
            if (!itemHasBeenDrawn)
            {
                //item has not been drawn yet
                itemPrefab = Instantiate(itemPrefab, transform);
                //itemPrefab. .transform(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
                Item item = itemPrefab.GetComponent<Item>();
                item.id = itemId;
                itemHasBeenDrawn = true;
            }
            else
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
   
    }
}
