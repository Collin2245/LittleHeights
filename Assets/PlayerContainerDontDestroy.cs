using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainerDontDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerContainerDontDestroy Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("creating inventory instance");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
