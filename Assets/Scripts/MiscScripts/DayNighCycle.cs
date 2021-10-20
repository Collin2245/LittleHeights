using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class DayNighCycle : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;
    Light2D globalLight;
    float r;
    float g;
    float b;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        globalLight = GetComponent<Light2D>();
        r = 0;
        g = 0;
        b = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //tmp.text = globalLight.color.ToString();
        tmp.text = "time:" + (Time.deltaTime).ToString();
        r += Time.deltaTime;
        g += Time.deltaTime;
        b += Time.deltaTime;
        //globalLight.color = new Color(r,g,b);
    }
}
