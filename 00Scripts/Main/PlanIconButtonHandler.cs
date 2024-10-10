using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanIconButtonHandler : DraggableButtonHandler
{
    [SerializeField] private Transform planIconParent;

    
    public Enums.PlanType planType;
    private Button button;
    private EventTrigger eventTrigger;
    
    [SerializeField] private float lastClickTimer = 0;
    
    [SerializeField] private TextMeshProUGUI titleText;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        eventTrigger = GetComponent<EventTrigger>();
        
        // double click
        button.onClick.AddListener(() =>
        {
            if (Time.time - lastClickTimer < 0.3f)
            {
                Debug.Log("Double Click");

                foreach (var plan in PlanManager.I.planList.Where(plan => plan.type == Enums.PlanType.None))
                {
                    Debug.Log("PlanManager.I.planList: " + plan.day);
                    var planSlot = PlanManager.I.GetPlanSlot(plan.day);
                        
                    var newPlanIcon = Instantiate(gameObject, transform.parent);
                        newPlanIcon.transform.position = transform.position;
                        newPlanIcon.name = gameObject.name;
        
                    transform.GetComponent<Image>().raycastTarget = false;
                    transform.SetParent(planIconParent);
                    
                    Set(planSlot);
                    
                    break;
                }
            }
            
            lastClickTimer = Time.time;
        });
        
        
        // mouse hover
        var entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        entry.callback.AddListener((data) => { OnMouseEnter(); });
        eventTrigger.triggers.Add(entry);
        
        entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
        entry.callback.AddListener((data) => { OnMouseExit(); });
        eventTrigger.triggers.Add(entry);
        
        entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
        entry.callback.AddListener((data) =>
        {
            SoundManager.I.PlaySFX_PlanSelect();
            OnMouseExit();
        });
        eventTrigger.triggers.Add(entry);
    }


    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        
        var newPlanIcon = Instantiate(gameObject, transform.parent);
            newPlanIcon.transform.position = transform.position;
            newPlanIcon.name = gameObject.name;
        
        transform.GetComponent<Image>().raycastTarget = false;
        transform.SetParent(planIconParent);
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        
        transform.position = eventData.position;
    }
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        
        // 해당 위치에 있는 Image를 가져온다.
        var planSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<PlanSlotHandler>();
        
        if (planSlot != null)
        {
            if (planSlot.isOccupied)
            {
                Destroy(gameObject);
            }
            else
            {
                Set(planSlot);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Set(PlanSlotHandler planSlot)
    {
        SoundManager.I.PlaySFX_PlanConfirm();
        
        planSlot.isOccupied = true;
                        
        transform.SetParent(planSlot.transform);
        transform.position = planSlot.transform.position;
        transform.tag = "PlanSlotButton";
        var button = transform.GetComponent<Button>();
        var image = transform.GetComponent<Image>();
            image.sprite = button.spriteState.highlightedSprite;
            
        PlanManager.I.ChangePlan(planSlot.transform.GetSiblingIndex() + 1, planType);

        string day = "";
        switch (planSlot.transform.GetSiblingIndex() + 1)
        {
            case 1:
                day = "월";
                break;
            case 2:
                day = "화";
                break;
            case 3:
                day = "수";
                break;
            case 4:
                day = "목";
                break;
            case 5:
                day = "금";
                break;
            case 6:
                day = "토";
                break;
            case 7:
                day = "일";
                break;
        }

        //await Task.Delay(500);

        switch (planType)
        {
            case Enums.PlanType.Exploration:
                LogManager.I.GenerateLog($"{day}요일에 바깥으로 나가보기로 했다.");
                break;
            case Enums.PlanType.Communication:
                LogManager.I.GenerateLog($"{day}요일은 함께 시간을 보내기로 했다.");

                
                break;
            case Enums.PlanType.Daily:
                LogManager.I.GenerateLog($"{day}요일은 집에서 보내기로 했다.");
                break;
        }
        
        PlanManager.I.CheckPlan();
    }
    
    private void OnMouseEnter()
    {
        titleText.gameObject.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        titleText.gameObject.SetActive(false);  
    }
}
