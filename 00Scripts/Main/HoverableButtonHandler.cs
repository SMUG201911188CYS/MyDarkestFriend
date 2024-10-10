using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverableButtonHandler : MonoBehaviour
{
    public EventTrigger eventTrigger;
    
    public Image hoverImage;
    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnPointerEnter(); });
        eventTrigger.triggers.Add(entry);
        
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { OnPointerExit(); });
        eventTrigger.triggers.Add(entry);
        
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerClick(); });
        eventTrigger.triggers.Add(entry);
    }
    
    public virtual void OnPointerEnter()
    {
        hoverImage.enabled = true;
    }
    
    public virtual void OnPointerExit()
    {
        hoverImage.enabled = false;
    }
    
    public virtual void OnPointerClick()
    {
    }
    
    public virtual void EnableEventTrigger()
    {
    }
}
