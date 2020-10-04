using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentItem : MonoBehaviour
{
    private GameObject hotbarSlot1;
    private GameObject hotbarSlot2;
    private GameObject hotbarSlot3;
    private GameObject hotbarSlot4;
    private GameObject hotbarSlot5;
    private GameObject hotbarSlot6;
    public GameObject currentSlot;
    private Image gameSlotIdentifier;
    public Dictionary<int, GameObject> hotBarSlotNum;
    public int currentSlotNum;
    public GameObject currentHotbarSlot;


    // Start is called before the first frame update
    void Start()
    {
        hotbarSlot1 = GameObject.Find("hotbarItem (1)");
        hotbarSlot2 = GameObject.Find("hotbarItem (2)");
        hotbarSlot3 = GameObject.Find("hotbarItem (3)");
        hotbarSlot4 = GameObject.Find("hotbarItem (4)");
        hotbarSlot5 = GameObject.Find("hotbarItem (5)");
        hotbarSlot6 = GameObject.Find("hotbarItem (6)");
        currentSlotNum = 1;
        currentSlot = hotbarSlot1;
        gameSlotIdentifier = this.GetComponent<Image>();
        hotBarSlotNum = new Dictionary<int, GameObject>()
        {
            {1, hotbarSlot1 },
            {2, hotbarSlot2 },
            {3, hotbarSlot3 },
            {4, hotbarSlot4 },
            {5, hotbarSlot5 },
            {6, hotbarSlot6 }
        };
    }

    // Update is called once per frame
    void Update()
    {
        currentSlot = hotBarSlotNum[currentSlotNum];
        this.transform.position = new Vector3(currentSlot.transform.position.x, currentSlot.transform.position.y, currentSlot.transform.position.z);
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            scrollDown();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            scrollUp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSlotNum = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSlotNum = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlotNum = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentSlotNum = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentSlotNum = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentSlotNum = 6;
        }
    }


    private void scrollUp()
    {
        if (currentSlotNum < 6)
        {
            currentSlotNum += 1;
        }
        else
        {
            currentSlotNum = 1;
        }
    }

    private void scrollDown()
    {
        if (currentSlotNum > 1)
        {
            currentSlotNum -= 1;
        }
        else
        {
            currentSlotNum = 6;
        }
    }

}
