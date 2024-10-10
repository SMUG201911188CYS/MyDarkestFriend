using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager I;

    private PlayerMovementHandler playerMovementHandler;
    private MinimapHandler minimapHandler;
    
    [SerializeField] private Image homeMapImage;
    [SerializeField] private Image walkMapImage;
    [SerializeField] private SpriteRenderer fieldBlackMapSpriteRenderer;
    [SerializeField] private SpriteRenderer fieldWhiteMapSpriteRenderer;
    [SerializeField] private SpriteRenderer fieldBlackMapGroundSpriteRenderer;

    [SerializeField] private CanvasGroup minimapCanvasGroup;
    
    [SerializeField] private TextMeshProUGUI mapNameText;

    
    public List<MapData> mapDataList;
    //public List<MapData> martMapDataList;
    public List<MapData> currentMapDataList;
    
    [SerializeField] private LeftToRightTransitionHandler leftToRightTransitionHandler;
    
    public string selectedMapName;
    
    [SerializeField] private GameObject[] martMapObjects;
    [SerializeField] private GameObject[] schoolMapObjects;
    [SerializeField] private GameObject npcGroup;
    
    public ObjectAnimationHandler mainMapAnimationHandler;
    
    [Header("TV")]
    [SerializeField] private GameObject tvArea;
    
    [Header("Select Map")]
    [SerializeField] private GameObject selectMapPanel;
    
    [Header("Select Map Image")]
    public Sprite [] selectMapImages; // 0 ~ , Enums 순서대로.
    private Image currentImage;
    
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(MapManager)) as MapManager;
        }

        currentImage = selectMapPanel.GetComponent<Image>();
        playerMovementHandler = FindObjectOfType<PlayerMovementHandler>();
        minimapHandler = FindObjectOfType<MinimapHandler>();
        
        mapDataList = new List<MapData>
        {
            new() { mapType = Enums.MapType.Bed, sprite1 = Resources.Load<Sprite>("Arts/Map/Home/Bed"), mapName = "침대", type = "Bed_"},
            new() { mapType = Enums.MapType.Bookshelf, sprite1 = Resources.Load<Sprite>("Arts/Map/Home/Bookshelf"), mapName = "책상", type = "Bookshelf_"},
            new() { mapType = Enums.MapType.Home, sprite1 = Resources.Load<Sprite>("Arts/Map/Home/Home"), mapName = "거실", type = "Home_"},
            new() { mapType = Enums.MapType.Yard, sprite1 = Resources.Load<Sprite>("Arts/Map/Daily/Yard"), mapName = "마당", type = "null"},
            new() { mapType = Enums.MapType.Tv, sprite1 = Resources.Load<Sprite>("Arts/Map/Home/TV"), mapName = "TV", type = "null"},
            new() { mapType = Enums.MapType.TvNone, sprite1 = Resources.Load<Sprite>("Arts/Map/Home/TV_None"), mapName = "TV", type = "null"},
            new() { mapType = Enums.MapType.Walk, sprite1 = Resources.Load<Sprite>("Arts/SD/SD_Walk/SD_Walk_BackGround"), mapName = "산책", type = "null"},
            new() { mapType = Enums.MapType.Mart_1, sprite1 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_1_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_1_2"), mapName = "식료품 코너", type = "Mart"},
            new() { mapType = Enums.MapType.Mart_2, sprite1 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_2_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_2_2"), mapName = "산지직송 코너", type = "Mart"},
            new() { mapType = Enums.MapType.Mart_3, sprite1 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_3_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_3_2"), mapName = "가공식품 코너", type = "Mart"},
            new() { mapType = Enums.MapType.Mart_4, sprite1 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_4_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_4_2"), mapName = "생산품 코너", type = "Mart"}, 
            new() { mapType = Enums.MapType.Mart_5, sprite1 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_5_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/Mart/Mart_5_2"), mapName = "마트 외부",type = "M_Start"},
            new() { mapType = Enums.MapType.School_1, sprite1 = Resources.Load<Sprite>("Arts/Map/School/School_1_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/School/School_1_2"), mapName = "복도", type = "School"},
            new() { mapType = Enums.MapType.School_2, sprite1 = Resources.Load<Sprite>("Arts/Map/School/School_2_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/School/School_2_2"), mapName = "1학년 교실", type = "School"},
            new() { mapType = Enums.MapType.School_3, sprite1 = Resources.Load<Sprite>("Arts/Map/School/School_3_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/School/School_3_2"), mapName = "2학년 교실", type = "School"},
            new() { mapType = Enums.MapType.School_4, sprite1 = Resources.Load<Sprite>("Arts/Map/School/School_4_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/School/School_4_2"), mapName = "음악실", type = "School"},
            new() { mapType = Enums.MapType.School_5, sprite1 = Resources.Load<Sprite>("Arts/Map/School/School_5_1"), sprite2 = Resources.Load<Sprite>("Arts/Map/School/School_5_2"), mapName = "학교 외부", type = "S_Start"},
            new() { mapType = Enums.MapType.Explore, mapName = "탐색", type = "Explore"}
        };

        
        
        //martButton.onClick.AddListener(OnClickMartButton);
        //schoolButton.onClick.AddListener(OnClickSchoolButton);
        
        
        
        ChangeMap(Enums.MapType.Home);
    }

    private void Update()
    {
        if (selectMapPanel.activeSelf)
        {
            Player.canControl = false;
        }
    }


    public void ChangeMap(Enums.MapType mapType)
    {
        var mapData = mapDataList.Find(x => x.mapType == mapType);
        
        mapNameText.text = mapData.mapName;
        
        if(ItemManager.I)
            ItemManager.I.SetItemObject(mapType);
        
        switch (mapType)
        {
            case Enums.MapType.Bed:
            case Enums.MapType.Bookshelf:
            case Enums.MapType.Home:
            case Enums.MapType.Yard :
                if(ItemManager.I)
                    ItemManager.I.currentMapType = CurrentMapType.Home;
                tvArea.SetActive(false);
                HideMinimap();

                if (mapType != Enums.MapType.Yard && WeatherManager.I != null)
                {
                    switch (WeatherManager.I.weather)
                    {
                        case Enums.WeatherType.눈:
                            mainMapAnimationHandler.PlayAnimation(mapData.type + "Snowy");
                            break;
                        case Enums.WeatherType.눈보라:
                            mainMapAnimationHandler.PlayAnimation(mapData.type + "Blizzard");
                            break;
                        case Enums.WeatherType.비:
                            mainMapAnimationHandler.PlayAnimation(mapData.type + "Rainy");
                            break;
                        case Enums.WeatherType.안개:
                            mainMapAnimationHandler.PlayAnimation(mapData.type + "Misty");
                            break;
                        case Enums.WeatherType.천둥번개:
                            mainMapAnimationHandler.PlayAnimation(mapData.type + "Thunder");
                            break;
                        case Enums.WeatherType.폭풍:
                            mainMapAnimationHandler.PlayAnimation(mapData.type + "Storm");
                            break;
                        case Enums.WeatherType.흐림:
                            mainMapAnimationHandler.PlayAnimation(mapData.type + "Cloudy");
                            break;
                        case Enums.WeatherType.None:
                        case Enums.WeatherType.맑음:
                        default:
                            mainMapAnimationHandler.StopAnimation();
                            break;
                        
                    }
                }
                
                homeMapImage.sprite = mapData.sprite1;
                homeMapImage.enabled = true;
                walkMapImage.enabled = false;
                
                foreach (var martMapObject in martMapObjects)
                {
                    martMapObject.SetActive(false);
                }
                foreach (var schoolMapObject in schoolMapObjects)
                {
                    schoolMapObject.SetActive(false);
                }
                break;
            case Enums.MapType.Walk :
                if(ItemManager.I)
                    ItemManager.I.currentMapType = CurrentMapType.Home;
                tvArea.SetActive(false);
                HideMinimap();
                
                homeMapImage.enabled = false;
                walkMapImage.enabled = true;
                mainMapAnimationHandler.StopAnimation();
                
                foreach (var martMapObject in martMapObjects)
                {
                    martMapObject.SetActive(false);
                }
                foreach (var schoolMapObject in schoolMapObjects)
                {
                    schoolMapObject.SetActive(false);
                }
                break;
            
            case Enums.MapType.Tv:
            case Enums.MapType.TvNone:
                ItemManager.I.currentMapType = CurrentMapType.Home;
                tvArea.SetActive(true);
                HideMinimap();

                BuyItemManager.I.ShowBuyItemList();
                BuyItemManager.I.Init();
                
                homeMapImage.sprite = mapData.sprite1;
                homeMapImage.enabled = true;
                walkMapImage.enabled = false;
                break;
            
            case Enums.MapType.Mart_1:
            case Enums.MapType.Mart_2:
            case Enums.MapType.Mart_3:
            case Enums.MapType.Mart_4:
            case Enums.MapType.Mart_5:
                ItemManager.I.currentMapType = CurrentMapType.Exploring;
                tvArea.SetActive(false);
                ShowMinimap();
                
                mainMapAnimationHandler.StopAnimation();
                
                // Field map sprite change
                fieldBlackMapSpriteRenderer.sprite = mapData.sprite1;
                fieldBlackMapGroundSpriteRenderer.sprite = mapData.sprite1;
                fieldWhiteMapSpriteRenderer.sprite = mapData.sprite2;
                
                homeMapImage.enabled = false;
                walkMapImage.enabled = false;

                foreach (var martMapObject in martMapObjects)
                {
                    martMapObject.SetActive(false);
                }

                switch (mapType)
                {
                    case Enums.MapType.Mart_1:
                        martMapObjects[0].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.Mart_2:
                        martMapObjects[1].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.Mart_3:
                        martMapObjects[2].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.Mart_4:
                        martMapObjects[3].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.Mart_5:
                        martMapObjects[4].SetActive(true);
                        break;
                }
                break;
            case Enums.MapType.School_1:
            case Enums.MapType.School_2:
            case Enums.MapType.School_3:
            case Enums.MapType.School_4:
            case Enums.MapType.School_5:
                ItemManager.I.currentMapType = CurrentMapType.Exploring;
                tvArea.SetActive(false);
                ShowMinimap();

                mainMapAnimationHandler.StopAnimation();
                
                // Field map sprite change
                fieldBlackMapSpriteRenderer.sprite = mapData.sprite1;
                fieldBlackMapGroundSpriteRenderer.sprite = mapData.sprite1;
                fieldWhiteMapSpriteRenderer.sprite = mapData.sprite2;
                
                homeMapImage.enabled = false;
                walkMapImage.enabled = false;

                foreach (var schoolMapObject in schoolMapObjects)
                {
                    schoolMapObject.SetActive(false);
                }

                switch (mapType)
                {
                    case Enums.MapType.School_1:
                        schoolMapObjects[0].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.School_2:
                        schoolMapObjects[1].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.School_3:
                        schoolMapObjects[2].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.School_4:
                        schoolMapObjects[3].SetActive(true);
                        LogManager.I.GenerateLog($"{mapData.mapName}으로 이동했다.");
                        break;
                    case Enums.MapType.School_5:
                        schoolMapObjects[4].SetActive(true);
                        break;
                }
                break;
            case Enums.MapType.Explore:
                ItemManager.I.currentMapType = CurrentMapType.None;
                walkMapImage.enabled = false;
                mainMapAnimationHandler.StopAnimation();
                ActiveSelectMapPanel();
                minimapHandler.Init();
                break;
        }
    }
    
    // Minimap
    public void ShowMinimap()
    {
        minimapCanvasGroup.alpha = 1;
    }
    
    public void HideMinimap()
    {
        minimapCanvasGroup.alpha = 0;
    }

    public int selectedMapIndex;

    private void SelectMiniMap()
    {
        switch (GetCurrentMapDataListCount() + 1)
        {
            case 4:
                Debug.Log("Select A");
                minimapHandler.SelectA(selectedMapIndex);
                break;
            
            case 3:
                Debug.Log("Select B");
                minimapHandler.SelectB(selectedMapIndex);
                break;
            
            case 2:
                Debug.Log("Select C");
                minimapHandler.SelectC(selectedMapIndex);
                break;
            
            case 1:
                Debug.Log("Select D");
                minimapHandler.SelectD(selectedMapIndex);
                break;
        }
    }
    
    public void GenerateCurrentMapDataList(String map)
    {
        currentMapDataList.Clear();
        
        currentMapDataList = mapDataList.FindAll(a => a.type.Contains(map));
        
        // Shuffle
        for (var i = 0; i < currentMapDataList.Count; i++)
        {
            var temp = currentMapDataList[i];
            var randomIndex = UnityEngine.Random.Range(i, currentMapDataList.Count);
            currentMapDataList[i] = currentMapDataList[randomIndex];
            currentMapDataList[randomIndex] = temp;
        }
    }
    public void RemoveEnterCurrentMap(string mapName)
    {
        currentMapDataList.Remove(currentMapDataList.Find(x => x.mapName == mapName));
        
        for (var i = 0; i < currentMapDataList.Count; i++)
        {
            var temp = currentMapDataList[i];
            var randomIndex = UnityEngine.Random.Range(i, currentMapDataList.Count);
            currentMapDataList[i] = currentMapDataList[randomIndex];
            currentMapDataList[randomIndex] = temp;
        }
    }
    public int GetCurrentMapDataListCount()
    {
        return currentMapDataList.Count;
    }
    
    public void LeftToRightTransition()
    {
        Main.I.SetCanPause(false);
        leftToRightTransitionHandler.gameObject.SetActive(true);
        leftToRightTransitionHandler.onAction.RemoveAllListeners();

        var onAction = new UnityEvent();
            onAction.AddListener(() =>
            {
                switch (selectedMapName)
                {
                    case "Home":
                        ChangeMap(Enums.MapType.Home);
                        playerMovementHandler.InitPosition();
                        DeactivateNpcGroup();
                        
                        SoundManager.I.PlayBGM_Main();
                        break;
                    
                    case "식료품 코너": // Mart
                        ChangeMap(Enums.MapType.Mart_1);
                        break;
                    case "산지직송 코너":
                        ChangeMap(Enums.MapType.Mart_2);
                        break;
                    case "가공식품 코너":
                        ChangeMap(Enums.MapType.Mart_3);
                        break;
                    case "생산품 코너":
                        ChangeMap(Enums.MapType.Mart_4);
                        break;
                    
                    case "복도": // School
                        ChangeMap(Enums.MapType.School_1);
                        break;
                    case "1학년 교실":
                        ChangeMap(Enums.MapType.School_2);
                        break;
                    case "2학년 교실":
                        ChangeMap(Enums.MapType.School_3);
                        break;
                    case "음악실":
                        ChangeMap(Enums.MapType.School_4);
                        break;
                }
                
                if (GetCurrentMapDataListCount() == 1)
                {
                    ActivateNpcGroup();
                }
                
                RemoveEnterCurrentMap(selectedMapName);
                
                playerMovementHandler.ChangeMapPosition();
                
                SelectMiniMap();
            });
        
        leftToRightTransitionHandler.onAction = onAction;
    }
    
    public void ActivateNpcGroup()
    {
        npcGroup.SetActive(true);
        npcGroup.GetComponent<NpcSpawner>().Spawn();
    }
    
    public void DeactivateNpcGroup()
    {
        npcGroup.SetActive(false);
    }
    
    // Select Map
    public void ActiveSelectMapPanel()
    {
        selectMapPanel.SetActive(true);
    }
    
    public void OnClickMapSelcetButton(Enums.MapSelectType selectMap)
    {
        switch (selectMap)
        {
            case Enums.MapSelectType.Mart:
                ChangeMap(Enums.MapType.Mart_5);
                break;
            case Enums.MapSelectType.School:
                ChangeMap(Enums.MapType.School_5);
                break;
            case Enums.MapSelectType.Hospital:
            case Enums.MapSelectType.AmusementPark:
            case Enums.MapSelectType.DepartmentStore:
                break;
        }
        
        ChangeCurrentImage(Enums.MapSelectType.Base);
        selectMapPanel.SetActive(false);
        
        Player.canControl = true;
    }
    
    public void ChangeCurrentImage(Enums.MapSelectType selectMap)
    {
        currentImage.sprite = selectMapImages[(int)selectMap];
    }
    
    public string GetCurrentMapName()
    {
        return mapNameText.text;
    }
    
}

[Serializable]
public class MapData
{
    public Enums.MapType mapType;
    public Sprite sprite1;
    public Sprite sprite2;
    public string mapName;
    public string type;
}