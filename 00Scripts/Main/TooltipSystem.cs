using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    private Vector2 pos;
    [SerializeField] private TooltipHandler tooltipHandler;

    public void Awake()
    {
        current = this;
    }

    private void Start()
    {
        current.gameObject.SetActive(false);
    }

    public static void Show(string tooltipText)
    {
        current.tooltipHandler.SetText(tooltipText);
        current.tooltipHandler.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltipHandler.gameObject.SetActive(false);
    }
}
