using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager I;

    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(DoorManager)) as DoorManager;
        }
    }
    
    public void Interact(string doorName)
    {
        string mapType;

        if (doorName.Contains("Mart"))
            mapType = "Mart";
        else if (doorName.Contains("School"))
            mapType = "School";
        else
            mapType = "Mart";
            
        
        switch (doorName)
        {
            case "Mart 5 In":
            case "School 5 In":
                Player.canControl = false;
                
                LogManager.I.GenerateLog("어디로 가볼까?");

                MapManager.I.GenerateCurrentMapDataList(mapType);
                
                LogManager.I.Button2TextChange(MapManager.I.currentMapDataList[0].mapName, MapManager.I.currentMapDataList[1].mapName);
                break;
            
            case "Mart 1 In":
            case "Mart 2 In":
            case "Mart 3 In":
            case "Mart 4 In":
            case "School 1 In":
            case "School 2 In":
            case "School 3 In":
            case "School 4 In":
                Player.canControl = false;
                
                LogManager.I.GenerateLog("집으로 돌아갈까?");
                
                LogManager.I.Button2TextChange("예", "아니오");
                
                break;
            
            case "Mart 1 Out":
            case "Mart 2 Out":
            case "Mart 3 Out":
            case "Mart 4 Out":
            case "School 1 Out":
            case "School 2 Out":
            case "School 3 Out":
            case "School 4 Out":
                Player.canControl = false;

                if (MapManager.I.GetCurrentMapDataListCount() == 0)
                {
                    LogManager.I.GenerateLog("집으로 돌아가자.");
                    
                    MapManager.I.selectedMapName = "Home";
                    
                    MapManager.I.LeftToRightTransition();
                    
                }
                else if (MapManager.I.GetCurrentMapDataListCount() == 1)
                {
                    var random = Random.Range(0, 3);

                    switch (random)
                    {
                        case 0:
                            LogManager.I.GenerateLog("주변에 사람이 있는 것 같다.");
                            break;
                        case 1:
                            LogManager.I.GenerateLog("인기척이 들려온다.");
                            break;
                        case 2:
                            LogManager.I.GenerateLog("발자국 소리가 들린다.");
                            break;
                    }

                    MapManager.I.selectedMapName = MapManager.I.currentMapDataList[0].mapName;
            
                    MapManager.I.LeftToRightTransition();
                }
                else
                {
                    LogManager.I.GenerateLog("어디로 가볼까?");

                    LogManager.I.Button2TextChange(MapManager.I.currentMapDataList[0].mapName, MapManager.I.currentMapDataList[1].mapName);
                }
                break;
        }
    }
}
