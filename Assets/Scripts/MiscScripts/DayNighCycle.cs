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
    float timer;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        globalLight = GetComponent<Light2D>();
        timer = 30;
    }

    // Update is called once per frame
    void Update()
    {
        //tmp.text = globalLight.color.ToString();
        tmp.text = "time:" + (timer += Time.deltaTime).ToString();
    }
}
