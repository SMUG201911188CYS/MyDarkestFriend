using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlanManager : MonoBehaviour
{
    public static PlanManager I;
    
    private PlayerShiftHandler playerShiftHandler;
    
    public List<PlanInfo> planList;
    public int currentDay;

    public List<ScheduleIconHandler> scheduleIconList;

    [SerializeField] private Transform planSlotGroup;
    [SerializeField] private List<PlanSlotHandler> planSlotList = new List<PlanSlotHandler>();
    
    [SerializeField] private CanvasGroup popupAreaCg;
    [SerializeField] private GameObject planButtonGroup;
    // Schedule Icon
    [SerializeField] private Sprite emptyPlanIcon;
    // 완료하지 않은 일정
    [SerializeField] private Sprite explorationNotCompleteIcon;
    [SerializeField] private Sprite communicationNotCompleteIcon;
    [SerializeField] private Sprite dailyNotCompleteIcon;
    // 완료한 일정
    [SerializeField] private Sprite explorationCompleteIcon;
    [SerializeField] private Sprite communicationCompleteIcon;
    [SerializeField] private Sprite dailyCompleteIcon;

    [SerializeField] private PlanResetButtonHandler resetBtn;
    [SerializeField] private PlanConfrimButtonHandler confirmBtn;

    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(PlanManager)) as PlanManager;
        }
        
        playerShiftHandler = FindObjectOfType<PlayerShiftHandler>();
        
        planList.Clear();
        
        planList = new List<PlanInfo>
        {
            new() { day = 1, type = Enums.PlanType.None },
            new() { day = 2, type = Enums.PlanType.None },
            new() { day = 3, type = Enums.PlanType.None },
            new() { day = 4, type = Enums.PlanType.None },
            new() { day = 5, type = Enums.PlanType.None },
            new() { day = 6, type = Enums.PlanType.None },
            new() { day = 7, type = Enums.PlanType.None }
        };
        
        for (var i = 0; i < planSlotGroup.childCount; i++)
        {
            var planSlot = planSlotGroup.GetChild(i).GetComponent<PlanSlotHandler>();
            planSlotList.Add(planSlot);
        }
    }

    private void Start()
    {
        Invoke(nameof(InitPlanList), 1.5f);
    }

    public void ResetPlanList()
    {
        var SlotButton = GameObject.FindGameObjectsWithTag("PlanSlotButton");
        foreach (var obj in SlotButton)
        {
            Destroy(obj.gameObject);
        }

        foreach (var planSlot in planSlotList)
        {
            planSlot.isOccupied = false;
        }
        
        if(planButtonGroup.activeInHierarchy)
            popupAreaCg.blocksRaycasts = true;
        
        ChangePlan(1, Enums.PlanType.None);
        ChangePlan(2, Enums.PlanType.None);
        ChangePlan(3, Enums.PlanType.None);
        ChangePlan(4, Enums.PlanType.None);
        ChangePlan(5, Enums.PlanType.None);
        ChangePlan(6, Enums.PlanType.None);
        ChangePlan(7, Enums.PlanType.None);
    }

    public void InitPlanList()
    {
        ResetPlanList();
        
        DateManager.I.SetDateText();
        
        if (EndingManager.I.CheckEnd())
            return;
        
        MapManager.I.mainMapAnimationHandler.StopAnimation();
        
        popupAreaCg.DOFade(1, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            popupAreaCg.blocksRaycasts = true;
            planButtonGroup.SetActive(true);
        });
        
        if (DateManager.I.date.year == 1 && DateManager.I.date.week == 1 && DateManager.I.date.day == "월" &&
            DateManager.I.date.season == "봄")
        {
            LogManager.I.GenerateLog("우리는 함께 살아가기로 했다.");
            LogManager.I.GenerateLog("이번 주에 할 일을 정해보자.");
            LogManager.I.GenerateLogWithColor("각 일정을 더블클릭하거나 드래그하여 일정을 배치할 수 있습니다.", Color.gray);
            LogManager.I.GenerateLogWithColor("각 일정은 주에 1회 이상 필수로 배치되어야 합니다.", Color.gray);
        }
        else
        {
            LogManager.I.GenerateLog("새로운 일주일의 시작이다.");
            LogManager.I.GenerateLog("이번 주에는 무엇을 할까?");
            
        }
    }
    
    public void ChangePlan(int day, Enums.PlanType type)
    {
        var planInfo = planList.Find(x => x.day == day);
        
        if (planInfo != null)
        {
            planInfo.type = type;
            
            var scheduleIcon = scheduleIconList[day - 1];

            switch (type)
            {
                case Enums.PlanType.Exploration:
                    scheduleIcon.SetIcon(explorationNotCompleteIcon);
                    break;
                case Enums.PlanType.Communication:
                    scheduleIcon.SetIcon(communicationNotCompleteIcon);
                    break;
                case Enums.PlanType.Daily:
                    scheduleIcon.SetIcon(dailyNotCompleteIcon);
                    break;
                case Enums.PlanType.None:
                    scheduleIcon.SetIcon(emptyPlanIcon);
                    break;
            }
            
            
        }
    }
    
    public void InitPlan(int day)
    {
        var planInfo = planList.Find(x => x.day == day);
        
        if (planInfo != null)
        {
            planInfo.type = Enums.PlanType.None;
            
            var scheduleIcon = scheduleIconList[day - 1];
                scheduleIcon.SetIcon(emptyPlanIcon);
        }
    }
    
    public void CheckPlan()
    {
        // planList 7개에서 Exploration, Communication, Daily 각각 1개 이상 있는지 확인
        var explorationCount = 0;
        var communicationCount = 0;
        var dailyCount = 0;

        if (planList.Any(planInfo => planInfo.type == Enums.PlanType.None)) return;
        
        foreach (var planInfo in planList)
        {
            switch (planInfo.type)
            {
                case Enums.PlanType.Exploration:
                    explorationCount++;
                    break;
                case Enums.PlanType.Communication:
                    communicationCount++;
                    break;
                case Enums.PlanType.Daily:
                    dailyCount++;
                    break;
            }
        }
        
        if (explorationCount > 0 && communicationCount > 0 && dailyCount > 0)
        {
            popupAreaCg.blocksRaycasts = false;
            confirmBtn.changeBtnState();
            resetBtn.ChangeText();
            resetBtn.isPlanFill = true;
        }
        else
        {
            if (explorationCount == 0)
            {
                LogManager.I.GenerateLog("하루쯤은 밖에 나가서 블랙이 먹을 것을 구해와야 한다.");
            }
            
            if (communicationCount == 0)
            {
                LogManager.I.GenerateLog("서로에게 더 시간을 주는 것이 좋지 않을까?");
            }
            
            if (dailyCount == 0)
            {
                LogManager.I.GenerateLog("화이트가 집에서 생활을 돌아볼 시간도 필요할 것이다.");
            }
            
            SoundManager.I.PlaySFX_Error();
        }
    }

    public void PlanEnd()
    {
        popupAreaCg.blocksRaycasts = false;
        planButtonGroup.SetActive(false);
        
        popupAreaCg.DOFade(0, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            currentDay = 1;
            
            // 월요일 일정 바로 시작
            Main.I.ActiveFadeInOutDimed();
            confirmBtn.changeBtnState();
            resetBtn.ChangeText();
            StartTodayPlan();
        });
    }
    
    public void NextDay()
    {
        DateManager.I.UpdateDate();
        
        WeatherManager.I.SetWeather();
        
        var planInfo = planList.Find(x => x.day == currentDay);
        
        switch (planInfo.type)
        {
            case Enums.PlanType.Exploration:
                scheduleIconList[currentDay - 1].SetIcon(explorationCompleteIcon);
                break;
            case Enums.PlanType.Communication:
                scheduleIconList[currentDay - 1].SetIcon(communicationCompleteIcon);
                break;
            case Enums.PlanType.Daily:
                scheduleIconList[currentDay - 1].SetIcon(dailyCompleteIcon);
                break;
        }
        
        currentDay++;
        StatManager.I.SettleRisk();
        CurrencyManager.I.ChangeStatText(Enums.PlayerStat.Attention);
        
        if (currentDay > 7)
        {
            // 다음 주로 넘어가기
            Main.I.ActiveFadeInOutDimed();
            CheckStatCondition();
            MapManager.I.ChangeMap(Enums.MapType.Home);
            //Player.playerStat[Enums.PlayerStat.Attention] = 100f;
            Invoke(nameof(InitPlanList), 1);
        }
        else
        {
            Main.I.ActiveFadeInOutDimed();
            StartTodayPlan();
        }
    }

    private Date currentDate;
    private PlanInfo todayPlan;
    
    public void StartTodayPlan()
    {
        currentDate = DateManager.I.date;
        todayPlan = planList[currentDay - 1];

        if (todayPlan.type == Enums.PlanType.Exploration)
        {
            SoundManager.I.PlayBGM_Exploration();
            // Player를 Black으로 변경
            playerShiftHandler.ShiftToBlack();
        }
        
        Invoke(nameof(A), 1);
    }

    private void A()
    {
        if (EndingManager.I.CheckEnd())
            return;
        
        switch (todayPlan.type)
        {
            case Enums.PlanType.Exploration:
                MapManager.I.ChangeMap(Enums.MapType.Explore);
                break;
            case Enums.PlanType.Communication:
                MapManager.I.ChangeMap(Enums.MapType.Home);
                break;
            case Enums.PlanType.Daily:
                MapManager.I.ChangeMap(Enums.MapType.Home);
                break;
        }
        
        Invoke(nameof(B), 1.5f);
    }

    private async void B()
    {
        LogManager.I.GenerateLog($"<color=#999>{currentDate.year}년차 {currentDate.season}의 {currentDate.week}주 {currentDate.day}요일</color>");
        DateManager.I.SetDateText();
        switch (todayPlan.type)
        {
            case Enums.PlanType.Exploration:
                LogManager.I.GenerateLog("해가 진 뒤에 밖으로 나왔다.");
                CurrencyManager.I.ChangeStatText(Enums.PlayerStat.Risk);
                
                if (popupAreaCg.alpha == 1)
                {
                    popupAreaCg.blocksRaycasts = false;
            
                    popupAreaCg.DOFade(0, 0.5f).SetEase(Ease.InOutSine);    
                }
                break;
            
            case Enums.PlanType.Communication:
                CommunicationManager.I.SelectCommunication();
                    
                CommunicationManager.I.StartConversation();
                break;
            
            case Enums.PlanType.Daily:
                LogManager.I.GenerateLog("오늘은 집에 있기로 했다.");
                LogManager.I.GenerateLog("무엇을 해야 할까?");
                
                await Task.Delay(1000);
                
                // 선택지 등장
                LogManager.I.Button3TextChange("구매", "독서", "활동");
                break;
        }
    }

    public PlanSlotHandler GetPlanSlot(int day)
    {
        return planSlotList[day - 1];
    }
    
    public Enums.PlanType GetPlanType()
    {
        return currentDay == 0 ? Enums.PlanType.None : planList.Find(x => x.day == currentDay).type;
    }

    public void CheckStatCondition()
    {
        if ( StatManager.I.eatNpcCount <= 2)
        {
            StatManager.I.ChangePlayerStat(Enums.PlayerStat.Humanity, 1f);
            StatManager.I.ChangePlayerStat(Enums.PlayerStat.Nature, -0.5f);
        }

        StatManager.I.eatNpcCount = 0;
    }
}

[Serializable]
public class PlanInfo
{
    public int day;
    public Enums.PlanType type;
}