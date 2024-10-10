using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButtonHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
    }
}
