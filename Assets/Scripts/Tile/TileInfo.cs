using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo
{
    // Start is called before the first frame update
    public bool isWater;
    public bool isWalkableWater;
    public bool isItemOn;
    public bool isTreeOn;
    public bool isGrass;
    




        public TileInfo()
    {
        this.isWater = false;
        this.isWalkableWater = false;
        this.isItemOn = false;
        this.isTreeOn = false;
        this.isGrass = true;
    }

}
