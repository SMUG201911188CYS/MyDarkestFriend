using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager I;
    
    [SerializeField] private Image weatherIcon;

    public Enums.WeatherType weather;
    public List<Weather> weatherList;
    public List<SeasonWeatherList> percentList;

    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(WeatherManager)) as WeatherManager;
        }
    }

    private void Start()
    {
        SetWeather();
    }

    public void SetWeather()
    {
        var random = UnityEngine.Random.Range(0, 100);

        var weatherPercentList = percentList.Find(x => x.season == DateManager.I.date.season).percent;
        int percentCal = 0;
        
        //Debug.Log(random);
        
        for (int i = 0; i < weatherPercentList.Length; i++)
        {
            percentCal += weatherPercentList[i].percent;
            //Debug.Log(weatherPercentList[i].percent);
            //Debug.Log(percentCal);
            if (random <= percentCal)
            {
                weather = weatherPercentList[i].type;
                weatherIcon.sprite = weatherList.Find(x => x.type == weather).icon;
                return;
            }
        }
    }
}

[Serializable]
public class Weather
{
    public Enums.WeatherType type;
    public Sprite icon;
}

[Serializable]
public class SeasonWeatherList
{
    public string season;
    public WeatherPercent[] percent;
}

[Serializable]
public class WeatherPercent
{
    public Enums.WeatherType type;
    public int percent;
}