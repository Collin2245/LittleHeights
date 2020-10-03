using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    // Start is called before the first frame update

    public static Hotbar Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("creating hotbar instance");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
