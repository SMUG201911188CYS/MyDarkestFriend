using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI log;
    private RectTransform rectTransform;
    
    public void SetLog(string logText)
    {
        log.text = logText;
    }
    
    public void SetColor(Color logTextColor)
    {
        log.color = logTextColor;
    }
    
    public void SetSize()
    {
        TryGetComponent(out rectTransform);

        rectTransform.sizeDelta = new Vector2(650f, 32f);
    }
}
