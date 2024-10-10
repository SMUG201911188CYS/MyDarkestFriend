using System;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        await Task.Delay(2500);
        
        PlanManager.I.currentDay = 1;
        
        PlanManager.I.ChangePlan(1, Enums.PlanType.Exploration);
        PlanManager.I.ChangePlan(2, Enums.PlanType.None);
        PlanManager.I.ChangePlan(3, Enums.PlanType.None);
        PlanManager.I.ChangePlan(4, Enums.PlanType.None);
        PlanManager.I.ChangePlan(5, Enums.PlanType.None);
        PlanManager.I.ChangePlan(6, Enums.PlanType.None);
        PlanManager.I.ChangePlan(7, Enums.PlanType.None);
        
        PlanManager.I.StartTodayPlan();
    }
}
