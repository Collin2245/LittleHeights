using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class DayNighCycle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float dayTime;
    [SerializeField]
    float dayToNightTime;
    [SerializeField]
    float nightTime;
    [SerializeField]
    float nightToDayTime;
    [SerializeField]
    Color dayColor;
    [SerializeField]
    Color nightColor;

    public float timeInDay;
    public float currentTime;
    public float currentDay;

    TextMeshProUGUI tmp;
    Light2D globalLight;
    public Vector3 dayTimeTransition;
    public Vector3 nightTimeTransition;

    void Start()
    {
        timeInDay = dayTime + dayToNightTime + nightTime + nightToDayTime;
        tmp = GetComponent<TextMeshProUGUI>();
        globalLight = GetComponent<Light2D>();
        dayTimeTransition = DayToNightColor(dayTime, dayToNightTime, dayColor, nightColor);
        nightTimeTransition = DayToNightColor(dayToNightTime, dayTime, nightColor, dayColor);
        globalLight.color = dayColor;
        InvokeRepeating("updateLighting", 0f, 1f);
    }

    void updateLighting()
    {
        if(currentTime <= dayTime)
        {
            return;
        }
        else if (currentTime <= dayToNightTime)
        {
            //move color from dayTimeTonightTime
            Vector3 temp = new Vector3(globalLight.color.r, globalLight.color.g, globalLight.color.b);
            globalLight.color =  new Color(temp.x + dayTimeTransition.x, temp.y + dayTimeTransition.y, temp.z + dayTimeTransition.z);
            return;
        }
        else if (currentTime <= nightTime)
        {
            return;
        }
        else if (currentTime <= nightToDayTime)
        {
            Vector3 temp = new Vector3(globalLight.color.r, globalLight.color.g, globalLight.color.b);
            globalLight.color = new Color(temp.x - nightTimeTransition.x, temp.y - nightTimeTransition.y, temp.z - nightTimeTransition.z);
            return;
        }
        else
        {
            currentTime = 0;
        }
    }
       
    Vector3 DayToNightColor(float start, float end, Color colorStart, Color colorEnd)
    {
        float timeframe = start - end;
        float r = ((colorStart.r - colorEnd.r) / timeframe);
        float g = ((colorStart.g - colorEnd.g) / timeframe);
        float b = ((colorStart.b - colorEnd.b) / timeframe);
        return new Vector3(r, g, b);
    }

    // Update is called once per frame
    void Update()
    {
        //tmp.text = globalLight.color.ToString();
        currentTime += Time.deltaTime;
        tmp.text = currentTime.ToString();
        
        //globalLight.color = new Color(r,g,b);
    }

    private void FixedUpdate()
    {
        //updateLighting();
    }
}
