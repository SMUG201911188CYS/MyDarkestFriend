using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LogButtonHandler : HoverableButtonHandler
{
    [SerializeField] private TextMeshProUGUI logButtonText;
    
    public async override void OnPointerClick()
    {
        eventTrigger.enabled = false;
        hoverImage.enabled = false;

        SoundManager.I.PlaySFX_Click();

        base.OnPointerClick();

        var selectedBookList = BookManager.I.selectedBookList;

        if (PlanManager.I.GetPlanType() == Enums.PlanType.Exploration)
        {
            switch (logButtonText.text)
            {
                case "예":
                    MapManager.I.selectedMapName = "Home";
                    MapManager.I.LeftToRightTransition();
                    break;
                case "아니오":
                    Player.canControl = true;
                    break;
                default:
                    MapManager.I.selectedMapName = logButtonText.text;
            
                    MapManager.I.selectedMapIndex = transform.GetSiblingIndex() + 1;
                    MapManager.I.LeftToRightTransition();
                    break;
                    
            }
            LogManager.I.Button2Inactive();
        }
        else if (CommunicationManager.I.CheckAnswer(logButtonText.text))
        {
            LogManager.I.Button2Inactive();
            LogManager.I.Button3Inactive();
        }
        if (selectedBookList.Count != 0)
        {
            switch (StatManager.I.growthQuarter)
            {
                case 1:
                    FindObjectOfType<CutsceneHandler>().SetSprite("독서");
                    break;
                case 2:
                    FindObjectOfType<CutsceneHandler>().SetSprite("독서_2");
                    break;
                default:
                    FindObjectOfType<CutsceneHandler>().SetSprite("독서");
                    break;
            }

            var index = 0;

            if (logButtonText.text == selectedBookList[0].title)
                index = 0;
            else if (logButtonText.text == selectedBookList[1].title)
                index = 1;
            else if (logButtonText.text == selectedBookList[2].title)
                index = 2;
            else
                index = 0;
            
            foreach (var description in selectedBookList[index].description)
            {
                LogManager.I.GenerateLog(description);
            }

            if (selectedBookList[index].conversation != null)
            {
                BookManager.I.StartConversation(index, selectedBookList[index].conversation);
            }
            else
            {
                BookManager.I.StartConversation();
            }
            
            LogManager.I.Button3Inactive();
        }
        else
        {
            switch (logButtonText.text)
            {
                case "구매":
                    LogManager.I.GenerateLog("홈쇼핑 방송을 틀었다. 부족한 물건이 있었던가? 무엇을 구매할까?");
                    LogManager.I.Button3Inactive();
                    
                    MapManager.I.ChangeMap(Enums.MapType.Tv);
                    break;
            
                case "독서":
                    
                    // 진입 시 해당 분기에 선택 가능한 책 중 랜덤으로 3개 선택지 출력
                    BookManager.I.SelectBooks();
                    selectedBookList = BookManager.I.selectedBookList;
                    /*
                    if (selectedBookList.Count == 0)
                    {
                        LogManager.I.GenerateLog("읽을 수 있는 책이 없다.");
                        LogManager.I.Button3Inactive();
                        await Task.Delay(2000);
                        
                        PlanManager.I.NextDay();
                        break;
                    }
                    */
                    LogManager.I.GenerateLog("읽고 싶은 책을 꺼내보자.");
                    MapManager.I.ChangeMap(Enums.MapType.Bookshelf);
                    
                    LogManager.I.Button3TextChange(selectedBookList[0].title, selectedBookList[1].title, selectedBookList[2].title);
                    break;
            
                case "활동":
                    LogManager.I.GenerateLog("하고 싶은 일을 정해보자.");
                    switch (StatManager.I.growthQuarter)
                    {
                        case 1:
                            LogManager.I.Button3TextChange("공놀이", "낮잠", "낙서");
                            break;
                        case 2:
                            LogManager.I.Button3TextChange("산책", "게임", "영화");
                            break;
                    }
                    break;
                
                case "공놀이":
                    MapManager.I.ChangeMap(Enums.MapType.Yard);
                    FindObjectOfType<CutsceneHandler>().SetSprite(logButtonText.text);
                    
                    ActivityManager.I.StartActivity(logButtonText.text);
                    break;
                
                case "낮잠":
                    MapManager.I.ChangeMap(Enums.MapType.Bed);
                    FindObjectOfType<CutsceneHandler>().SetSprite(logButtonText.text);
                    
                    ActivityManager.I.StartActivity(logButtonText.text);
                    break;
                
                case "낙서":
                    MapManager.I.ChangeMap(Enums.MapType.Bookshelf);
                    FindObjectOfType<CutsceneHandler>().SetSprite(logButtonText.text);
                    
                    ActivityManager.I.StartActivity(logButtonText.text);
                    break;
                case "산책":
                    MapManager.I.ChangeMap(Enums.MapType.Walk);
                    FindObjectOfType<CutsceneHandler>().walkSD.SetActive(true);
                    FindObjectOfType<BackgroundScrollHandler>().enabled = true;
                    ActivityManager.I.StartActivity(logButtonText.text);
                    break;
                
                case "게임":
                case "영화":
                    MapManager.I.ChangeMap(Enums.MapType.Home);
                    FindObjectOfType<CutsceneHandler>().SetSprite(logButtonText.text);
                    
                    ActivityManager.I.StartActivity(logButtonText.text);
                    break;
                
                
                // 공놀이
                case "세게 던진다.":
                case "살살 던진다.":
                // 낙서
                case "거칠게":
                case "기분 따라":
                case "열심히":
                // 산책
                case "천천히 걷는다.":
                case "빨리 걷는다.":
                // 영화
                case "로맨스":
                case "액션":
                case "공포":
                // 게임
                case "이지 모드":
                case "하드 모드":
                    ActivityManager.I.CheckChoice(logButtonText.text);
                    break;
                
            }
        }
        
        // 1초 뒤에 다시 활성화
        Invoke(nameof(EnableEventTrigger), 1f);
        
        logButtonText.color = Color.white;
    }

    public override void EnableEventTrigger()
    {
        base.EnableEventTrigger();
        
        eventTrigger.enabled = true;
    }

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
        
        logButtonText.color = Color.black;
    }
    
    public override void OnPointerExit()
    {
        base.OnPointerExit();
        
        logButtonText.color = Color.white;
    }
}
