using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager I;

    public int currency;
    public TextMeshProUGUI currencyText;
    public List<StatText> statText;
    public RiskProgressBarHandler bar;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(CurrencyManager)) as CurrencyManager;
        }
    }

    private void Start()
    {
        InitCurrency();

        foreach (var stat in Enum.GetValues(typeof(Enums.PlayerStat)))
        {
            SetStatCurrency((Enums.PlayerStat)stat);
            
            if ((Enums.PlayerStat)stat == Enums.PlayerStat.Attention)
                break;
        }
    }

    private void InitCurrency()
    {
        SetCurrency(50);
    }
    
    public void AddCurrency(int amount)
    {
        currency += amount;
        
        currencyText.text = currency.ToString("N0");

        if (currency <= 30) 
            return;
        
        DangerAlertManager.I.currencyAlert = false;
        DangerAlertManager.I.Set();
    }
    
    public void SubtractCurrency(int amount)
    {
        currency -= amount;
        
        currencyText.text = currency.ToString("N0");

        if (currency > 30)
            return;
        
        DangerAlertManager.I.currencyAlert = true;
        DangerAlertManager.I.Set();
    }
    
    public void SetCurrency(int amount)
    {
        currency = amount;
        
        currencyText.text = currency.ToString("N0");
        
        DangerAlertManager.I.currencyAlert = currency <= 30;
        DangerAlertManager.I.Set();
    }

    public void SetStatCurrency(Enums.PlayerStat changeStat)
    {
        switch (changeStat)
        {
            case Enums.PlayerStat.None:
                return;
            case Enums.PlayerStat.Risk:
            case Enums.PlayerStat.Attention:
                bar.SetProgress(StatManager.I.GetPlayerStat(changeStat));
                return;
            default:
                statText.Find(x => x.stat == changeStat).currencyText.text = StatManager.I.GetPlayerStat(changeStat).ToString();
                break;
        }
    }

    public void ChangeStatText(Enums.PlayerStat target)
    {
        bar.SetProgress(StatManager.I.GetPlayerStat(target));
    }

}

[Serializable]
public class StatText : Stat
{
    public TextMeshProUGUI currencyText;

}
