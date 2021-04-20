using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDict : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<Vector3Int, TileInfo> tileInfo;
    public static TileDict Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("creating tile dictionary instance");
            Instance = this;
            Instance.tileInfo = new Dictionary<Vector3Int, TileInfo>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //void Start()
    //{
    //    tileInfo = new Dictionary<Vector3Int, TileInfo>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
