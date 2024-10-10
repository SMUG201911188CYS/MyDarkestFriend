using TMPro;
using UnityEngine;

public class PlayerAreaButtonHandler : HoverableButtonHandler
{
    [SerializeField] private TextMeshProUGUI korText;
    [SerializeField] private TextMeshProUGUI engText;
    
    [SerializeField] private Color korNormalColor;
    [SerializeField] private Color korHoverColor;
    
    [SerializeField] private Color engNormalColor;
    [SerializeField] private Color engHoverColor;
    
    public override void OnPointerClick()
    {
        base.OnPointerClick();
        
        Debug.Log($"{korText.text} 클릭됨");

        if (korText.text == "속삭임")
        {
            WhisperManager.I.OnClick();
        }

        if (korText.text == "가방")
        {
            Debug.Log("가방을 연다");
        }
    }

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
        
        korText.color = korHoverColor;
        engText.color = engHoverColor;
    }
    
    public override void OnPointerExit()
    {
        base.OnPointerExit();
        
        korText.color = korNormalColor;
        engText.color = engNormalColor;
    }
}
