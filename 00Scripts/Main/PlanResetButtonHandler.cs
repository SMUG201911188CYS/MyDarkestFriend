using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanResetButtonHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    private Button button;
    
    public bool isPlanFill = false;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        SoundManager.I.PlaySFX_Click();
        PlanManager.I.ResetPlanList();
        
        if (isPlanFill)
        {
            ChangeText();
            isPlanFill = false;
            FindObjectOfType<PlanConfrimButtonHandler>().changeBtnState();
        }
    }

    public void ChangeText()
    {
        if (txt.text == "돌아가기")
            txt.text = "초기화";
        else
            txt.text = "돌아가기";
    }
    
}
