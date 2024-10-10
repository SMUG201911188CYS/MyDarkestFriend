using System;
using TMPro;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    public static DateManager I;

    public Date date;
    
    [SerializeField] private TextMeshProUGUI yearText;
    [SerializeField] private TextMeshProUGUI seasonText;
    [SerializeField] private TextMeshProUGUI weekText;
    [SerializeField] private TextMeshProUGUI dayText;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(DateManager)) as DateManager;
        }
        
        NewDate();
    }

    private void NewDate()
    {
        var newDate = new Date { year = 1, season = "봄", week = 1, day = "월" };
        
        date = newDate;
        
        SetDateText();
    }
    
    public void SetDateText()
    {
        yearText.text = date.year.ToString();
        seasonText.text = date.season;
        weekText.text = date.week.ToString();
        dayText.text = date.day;
    }

    public void UpdateDate()
    {
        if (date.day == "일")
        {
            if (date.week == 4)
            {
                if (date.season == "겨울")
                {
                    date.year++;

                    switch (date.year)
                    {
                        case 4:
                            StatManager.I.ChangeGrowthQuarter();
                            break;
                        case 8:
                            StatManager.I.ChangeGrowthQuarter();
                            break;
                        default:
                            break;
                    }
                    
                    date.season = "봄";
                    date.week = 1;
                }
                else
                {
                    date.season = date.season switch
                    {
                        "봄" => "여름",
                        "여름" => "가을",
                        "가을" => "겨울",
                        _ => date.season
                    };
                    
                    date.week = 1;
                }
            }
            else
            {
                date.week++;
            }
            
            date.day = "월";
        }
        else
        {
            date.day = date.day switch
            {
                "월" => "화",
                "화" => "수",
                "수" => "목",
                "목" => "금",
                "금" => "토",
                "토" => "일",
                _ => date.day
            };
        }
    }
}

[Serializable]
public class Date
{
    public int year;
    public string season;
    public int week;
    public string day; 
}