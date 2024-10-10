using UnityEngine;
using UnityEngine.UI;

public class PlanSlotHandler : MonoBehaviour
{
    private Button button;
    
    public bool isOccupied = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        Debug.Log("PlanSlotHandler OnClick");
        
        if (isOccupied)
        {
            Debug.Log("PlanSlotHandler OnClick isOccupied");
            
            isOccupied = false;
            
            PlanManager.I.InitPlan(transform.GetSiblingIndex() + 1);
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
