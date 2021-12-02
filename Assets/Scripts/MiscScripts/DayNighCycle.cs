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
    public bool isNight;

    TextMeshProUGUI tmp;
    Light2D globalLight;
    public Vector3 dayTimeTransition;
    public Vector3 nightTimeTransition;

    private void Awake()
    {
        currentTime = PersistentData.Instance.CurrentWorld.worldTime.time;
        currentDay = PersistentData.Instance.CurrentWorld.worldTime.day;
    }

    void Start()
    {
        
        timeInDay = nightToDayTime;
        tmp = GetComponent<TextMeshProUGUI>();
        globalLight = GetComponent<Light2D>();
        dayTimeTransition = DayToNightColor(dayTime, dayToNightTime, dayColor, nightColor);
        nightTimeTransition = DayToNightColor(nightTime, nightToDayTime, nightColor, dayColor);
        globalLight.color = dayColor;
        LoadDayColor();
        InvokeRepeating("updateLighting", 0f, 1f);
    }

    void LoadDayColor()
    {
        for(int i = 0; i <= currentTime; i ++)
        {
            updateLightingIndex(i);
        }
    }
    

    void updateLighting()
    {
        if(currentTime <= dayTime)
        {
            isNight = false;
            return;
        }
        else if (currentTime <= dayToNightTime)
        {
            //move color from dayTimeTonightTime
            Vector3 temp = new Vector3(globalLight.color.r, globalLight.color.g, globalLight.color.b);
            globalLight.color =  new Color(temp.x - dayTimeTransition.x, temp.y - dayTimeTransition.y, temp.z - dayTimeTransition.z);
            isNight = false;
            return;
        }
        else if (currentTime <= nightTime)
        {
            isNight = true;
            return;
        }
        else if (currentTime <= nightToDayTime)
        {
            Vector3 temp = new Vector3(globalLight.color.r, globalLight.color.g, globalLight.color.b);
            globalLight.color = new Color(temp.x - nightTimeTransition.x, temp.y - nightTimeTransition.y, temp.z - nightTimeTransition.z);
            isNight = true;
            return;
        }
        else
        {
            currentTime = 0;
            currentDay += 1;
        }
    }

    void updateLightingIndex(int timeInput)
    {
        if (timeInput <= dayTime)
        {
            isNight = false;
            return;
        }
        else if (timeInput <= dayToNightTime)
        {
            //move color from dayTimeTonightTime
            Vector3 temp = new Vector3(globalLight.color.r, globalLight.color.g, globalLight.color.b);
            globalLight.color = new Color(temp.x - dayTimeTransition.x, temp.y - dayTimeTransition.y, temp.z - dayTimeTransition.z);
            isNight = false;
            return;
        }
        else if (timeInput <= nightTime)
        {
            isNight = true;
            return;
        }
        else if (timeInput <= nightToDayTime)
        {
            Vector3 temp = new Vector3(globalLight.color.r, globalLight.color.g, globalLight.color.b);
            globalLight.color = new Color(temp.x - nightTimeTransition.x, temp.y - nightTimeTransition.y, temp.z - nightTimeTransition.z);
            isNight = true;
            return;
        }
        else
        {
            Debug.LogError("You should not be here");
            //currentTime = 0;
            //currentDay += 1;
        }
    }

    Vector3 DayToNightColor(float start, float end, Color colorStart, Color colorEnd)
    {
        float timeframe = end - start;
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
        tmp.text = "Day: " + currentDay + " Time: " + Mathf.Round(currentTime).ToString();
        if(Input.GetKeyDown(KeyCode.P))
        {
            WorldTime wt = new WorldTime();
            wt.day = currentDay;
            wt.time = currentTime;
            PersistentData.Instance.CurrentWorld.worldTime = wt;
            SaveHelper.SaveWorld(PersistentData.Instance.CurrentWorld);
        }
        //globalLight.color = new Color(r,g,b);
    }
}
