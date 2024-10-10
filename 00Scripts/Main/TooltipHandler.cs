using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private TextMeshProUGUI tooltipField;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private int characterWrapLimit;

    public void SetText(string tooltipString)
    {
        int tooltipLength = tooltipField.text.Length;

        layoutElement.enabled = (tooltipLength > characterWrapLimit) ? true : false; //툴팁 백그라운드 크기 글자에 맞추기
        
        tooltipText.text = tooltipString;
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;
        transform.position = position;
    }
}
