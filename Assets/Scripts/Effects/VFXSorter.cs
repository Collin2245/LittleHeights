using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSorter : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer renderer1;
    [SerializeField]
    int sortingLayer;
    public GameObject positionToFollow;
    bool flag;
    void Start()
    {
        flag = false;
        renderer1 = this.GetComponent<Renderer>();

        renderer1.sortingLayerName = "SomeRenderLayer";
        renderer1.sortingOrder = sortingLayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!flag)
        {
            this.transform.position = positionToFollow.transform.position;
            flag = true;
        }
        renderer1.sortingLayerName = "SomeRenderLayer";
        renderer1.sortingOrder = sortingLayer;
    }
}
