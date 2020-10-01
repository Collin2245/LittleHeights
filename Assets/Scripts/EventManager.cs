using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public static EventManager Instance { get; private set;}
    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Debug.Log("creating Event manager instance");
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}



    public static UnityEvent itemOnMouse;
}
