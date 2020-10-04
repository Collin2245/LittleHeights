using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuantities : MonoBehaviour
{
    public static Dictionary<string, int> quantityForItem = new Dictionary<string, int>()
    {
        { "Blank", 1 },
        {"Square",2 },
        {"woodenSword",1 }
    };
}



