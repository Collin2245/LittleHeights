using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GrassTile", menuName = "ScriptableObjects/Tiles/GrassTile", order = 1)]
public class GrassTile : TileBase
{
    public bool isPlaceable;
    public Sprite sprites;

}
