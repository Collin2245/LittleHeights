using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap baseMap;
    public TileBase tileToUse;
    void Start()
    {
        baseMap.BoxFill(new Vector3Int(0, 0, 0), tileToUse, 0, 0, 100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
