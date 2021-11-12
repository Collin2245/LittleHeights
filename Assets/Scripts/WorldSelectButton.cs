using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject content;
    ToggleGroup tg;
    void Start()
    {
        tg = content.GetComponent<ToggleGroup>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!tg.AnyTogglesOn())
        {
            GetComponent<Button>().interactable = false;
        }else
        {
            GetComponent<Button>().interactable = true;
        }
    }
}
