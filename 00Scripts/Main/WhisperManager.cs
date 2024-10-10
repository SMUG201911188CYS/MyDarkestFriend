using System;
using UnityEngine;

public class WhisperManager : MonoBehaviour
{
    public static WhisperManager I;
    

    private void Awake()
    {
        I = this;
    }

    public void OnClick()
    {
        var planType = PlanManager.I.GetPlanType();
        var currentMapName = MapManager.I.GetCurrentMapName();
        var playerType = Player.playerType;
        

        switch (planType)
        {
            case Enums.PlanType.Exploration:
                if (currentMapName == "마트 외부")
                {
                    LogManager.I.GenerateLog(playerType == Enums.PlayerType.Black ? "W: 들키지 않게 조심하자, 블랙." : "B: 조용히, 살금살금.");
                    return;
                }
                
                if (currentMapName is "식료품 코너" or "산지직송 코너" or "가공식품 코너" or "생산품 코너")
                {
                    // 근처에 NPC 있으면
                    //var mapCount = MapManager.I.GetMartMapDataListCount();
                    var mapCount = MapManager.I.GetCurrentMapDataListCount();
                    if (mapCount == 1)
                    {
                        LogManager.I.GenerateLog(playerType == Enums.PlayerType.Black ? "W: 누가 있는 것 같아." : "B: 먹잇감. 근처.");
                        return;
                    }
                }
                break;
            
            case Enums.PlanType.Communication:
                if (currentMapName == "거실")
                {
                    LogManager.I.GenerateLog(playerType == Enums.PlayerType.Black ? "W: 블랙, 이리 와." : "B: 그르르...");
                    return;
                }
                break;
            
            case Enums.PlanType.Daily:
                if (currentMapName == "마당")
                {
                    LogManager.I.GenerateLog(playerType == Enums.PlayerType.Black ? "W: 잘할 수 있을까?" : "B: 던진다, 받는다.");
                    return;
                }
                break;
        }
        
        LogManager.I.GenerateLog(playerType == Enums.PlayerType.Black ? "지금은 딱히 화이트가 할 말이 없는 것 같다." : "지금은 딱히 블랙이 할 말이 없는 것 같다.");
    }
}
