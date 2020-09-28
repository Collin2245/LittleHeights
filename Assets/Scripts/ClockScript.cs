using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{

    public static ClockScript Instance { get; private set; }
    public double time;
    public double seconds;
    public double minutes;
    public double hours;
    public double days;
    public int dayLength;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(time < 60)
        {
            time += 1.2;
        }
        else
        {
            time = 0;
            seconds += 1;
            if(seconds >= 60)
            {
                minutes += 1;
                seconds = 0;
            }
        }
    }
}
