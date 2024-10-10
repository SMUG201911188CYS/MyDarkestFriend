using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DangerAlertManager : MonoBehaviour
{
    public static DangerAlertManager I;

    [SerializeField] private CanvasGroup cg;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform box;
    
    // 주목도
    public bool attentionAlert;
    // 위험도
    public bool dangerAlert;
    // 재화
    public bool currencyAlert;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(DangerAlertManager)) as DangerAlertManager;
        }
    }

    public void Set()
    {
        cg.alpha = 0;
        text.text = "";
        
        if (attentionAlert)
        {
            text.text = "세간의 주목";
            box.sizeDelta = new Vector2(59.84f, box.sizeDelta.y);
            
            cg.alpha = 1;
        }
        
        if (dangerAlert)
        {
            text.text = "발각 위험";
            box.sizeDelta = new Vector2(49.1f, box.sizeDelta.y);
            cg.alpha = 1;
        }
        
        if (currencyAlert)
        {
            text.text = "빈곤";
            box.sizeDelta = new Vector2(29.11f, box.sizeDelta.y);
            cg.alpha = 1;
        }
        
    }
}
