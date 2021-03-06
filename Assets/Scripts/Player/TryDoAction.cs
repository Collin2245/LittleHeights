﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryDoAction : MonoBehaviour
{
    // Start is called before the first frame update
    TileManager tileManager;
    TreeScript treeScript;
    Vector3Int point;
    TileInfo tileInfo;
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        treeScript = GetComponent<TreeScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0) && tileManager.GetComponent<MouseHoverScript>().isActiveArea)
        {
            point = tileManager.baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            tileInfo = tileManager.GetTileInfoAtPoint();
            TryChopTree();

        }
        else
        {
            treeScript.resetCounter();
        }
    }

    void TryChopTree()
    {
        if (tileInfo.isTreeOn)
        {
            Debug.Log("HitTree");
            treeScript.TryChopTree(point, tileManager);
        }
    }
}
