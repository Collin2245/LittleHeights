using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public string itemId;
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Items/" + itemId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
