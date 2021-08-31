using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryDoAction : MonoBehaviour
{
    // Start is called before the first frame update
    TileManager tileManager;
    DestroyObject destroyObject;
    Vector3Int point;
    TileInfo tileInfo;
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        destroyObject = GetComponent<DestroyObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && tileManager.GetComponent<MouseHoverScript>().isActiveArea)
        {
            point = tileManager.baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            tileInfo = tileManager.GetTileInfoAtPoint();
            TryDestroyObject();
        }
        else
        {
            destroyObject.resetCounter();
        }
    }

    void TryDestroyObject()
    {
        if (tileInfo.isTreeOn)
        {
            Debug.Log("HitDestructableOb");
            destroyObject.TryDestroyObject(point, tileManager, "tree");
        }
    }
}
