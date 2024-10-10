using UnityEngine;
using UnityEngine.Events;

public class LeftToRightTransitionHandler : MonoBehaviour
{
    public UnityEvent onAction;

    public void OnAction()
    {
        onAction?.Invoke();
    }
    
    public void OnComplete()
    {
        if (MapManager.I.selectedMapName == "Home")
        {
            PlanManager.I.NextDay();
        }
        else
        {
            Player.canControl = true;
        }
        
        Main.I.SetCanPause(true);
        gameObject.SetActive(false);
    }
}
