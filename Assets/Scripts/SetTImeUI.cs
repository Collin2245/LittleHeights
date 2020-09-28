using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTimeUI : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    double  minutes;
    double  seconds;

    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        seconds = ClockScript.Instance.seconds;
        minutes = ClockScript.Instance.minutes;
        string timeMessage = "min: " + minutes.ToString() + " seconds: " + seconds.ToString();
        text.text = timeMessage;
    }
}
