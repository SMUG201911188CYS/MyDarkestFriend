using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager I;
    public int growthQuarter = 1;
    public int eatNpcCount = 0;
    
    [SerializeField] private Dictionary<Enums.PlayerStat, float> playerStat = new Dictionary<Enums.PlayerStat, float>()
    {
        { Enums.PlayerStat.Humanity, 0 },
        { Enums.PlayerStat.Dependency, 0 },
        { Enums.PlayerStat.Knowledge, 0 },
        { Enums.PlayerStat.Trust, 0 },
        { Enums.PlayerStat.Nature, 0 },
        { Enums.PlayerStat.Acquisition, 0 },
        { Enums.PlayerStat.Attention, 0 },
        { Enums.PlayerStat.Risk, 0 },
        { Enums.PlayerStat.DoNotEatNpc, 0 },
        { Enums.PlayerStat.Hunger, 0 },
        { Enums.PlayerStat.SerialHunger, 0 },
        { Enums.PlayerStat.None, 0 },
    };
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(StatManager)) as StatManager;
        }
    }
    
    public void ChangePlayerStat(Enums.PlayerStat stat, float value)
    {
        int maxLimit = 0, minLimit = 0;
        bool check = true;
        switch (stat)
        {
            case Enums.PlayerStat.Trust :
                maxLimit = 50;
                minLimit = -50;
                break;
            case Enums.PlayerStat.Attention :
                maxLimit = 100;
                minLimit = 0;
                break;
            case Enums.PlayerStat.Humanity :
            case Enums.PlayerStat.Dependency:
            case Enums.PlayerStat.Nature:
                maxLimit = growthQuarter == 1 ? 25 : 50;
                minLimit = growthQuarter == 1 ? -25 : -50;
                break;
            case Enums.PlayerStat.Knowledge :
            case Enums.PlayerStat.Acquisition :
                minLimit = 0;
                maxLimit = growthQuarter switch
                {
                    1 => 30,
                    2 => 60,
                    3 => 90,
                    4 => 100,
                    _ => 30
                };
                break;
            case Enums.PlayerStat.Risk:
                minLimit = 0;
                maxLimit = 100;
                break;
            case Enums.PlayerStat.DoNotEatNpc :
            case Enums.PlayerStat.Hunger :
            case Enums.PlayerStat.SerialHunger :
                check = false;
                break;
        }

        if (!check)
        {
            if (stat == Enums.PlayerStat.SerialHunger) // 1이면 연속, 0이면 초기화
                playerStat[stat] = value == 1f ? playerStat[stat] + 1 : 0;
            else
                playerStat[stat] += value;
            return;
        }
        
        if (value > 0)
            playerStat[stat] = playerStat[stat] + value > maxLimit ? maxLimit : playerStat[stat] + value;
        else
            playerStat[stat] = playerStat[stat] + value < minLimit ? minLimit : playerStat[stat] + value;
        
        CurrencyManager.I.SetStatCurrency(stat);
    }

    public float GetPlayerStat(Enums.PlayerStat stat)
    {
        return playerStat[stat];
    }
    
    public void SettleRisk()
    {
        playerStat[Enums.PlayerStat.Attention] += playerStat[Enums.PlayerStat.Risk] / 10f;
        playerStat[Enums.PlayerStat.Risk] = 0;
    }

    public void ChangeGrowthQuarter(bool isUp = true)
    {
        if (isUp)
            growthQuarter++;
        else
        {
            if (growthQuarter <= 1)
                growthQuarter = 1;
            else
                growthQuarter--;
        }
        
        FindObjectOfType<PlayerAnimationHandler>().ChangeAnimator();
    }
}
