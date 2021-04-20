using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryDoAction : MonoBehaviour
{
    // Start is called before the first frame update
    TileManager tileManager;
    TreeScript treeScript;
    Vector3Int point;
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        treeScript = new TreeScript();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0) && tileManager.GetComponent<MouseHoverScript>().isActiveArea)
        {
            point = tileManager.baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            TileInfo tileInfo = tileManager.GetTileInfoAtPoint();
            if(tileInfo.isTreeOn)
            {
                Debug.Log("HitTree");
                treeScript.TryChopTree(point, tileManager);
            }
        }
    }

    void ChopTree()
    {

    }
}
