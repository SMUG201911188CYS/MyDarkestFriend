using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeHandler : MonoBehaviour
{
    public Image fadeImage;
    
    [SerializeField] private Enums.FadeType fadeType;
    public bool isBlack;
    
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    
    [SerializeField] private float waitTime;
    
    [Header("Events")]
    [SerializeField] private float fadeInEventWaitTime;    
    public UnityEvent onCompleteFadeIn;
    [SerializeField] private float fadeOutEventWaitTime;    
    public UnityEvent onCompleteFadeOut;

    public bool isCanavas = false;
    public bool isHalf = false;
    
    public virtual void Start()
    {
        fadeImage = GetComponent<Image>();

        switch (fadeType)
        {
            case Enums.FadeType.In:
            case Enums.FadeType.InOut:
                StartCoroutine(FadeIn());
                break;
            
            case Enums.FadeType.Out:
            case Enums.FadeType.OutIn:
                StartCoroutine(FadeOut());
                break;
        }
    }
    
    private IEnumerator FadeIn()
    {
        var startColor = isBlack ? new Color(0, 0, 0, 0) : new Color(1, 1, 1, 0);

        fadeImage.color = startColor;
        
        // fadeInTime 동안 fadeImage의 alpha값을 1로 변경
        var time = 0f;
        while (time < fadeInTime)
        {
            if (!isHalf)
            {
                fadeImage.color = isBlack
                    ? new Color(0, 0, 0, Mathf.Lerp(0, 1, time / fadeInTime))
                    : new Color(1, 1, 1, Mathf.Lerp(0, 1, time / fadeInTime));
            }
            else
            {
                fadeImage.color = isBlack
                    ? new Color(0, 0, 0, Mathf.Lerp(0, 0.5f, time / fadeInTime))
                    : new Color(1, 1, 1, Mathf.Lerp(0, 0.5f, time / fadeInTime));
            }
            
            time += Time.deltaTime;
            
            yield return null;
        }
        
        if (fadeType is Enums.FadeType.In or Enums.FadeType.InOut or Enums.FadeType.OutIn)
        {
            // fadeInEventWaitTime 동안 대기
            yield return new WaitForSeconds(fadeInEventWaitTime);
            
            onCompleteFadeIn?.Invoke();
        }

        if (fadeType == Enums.FadeType.InOut)
        {
            // waitTime 동안 대기
            yield return new WaitForSeconds(waitTime);
            
            StartCoroutine(FadeOut());
        }
    }
    
    private IEnumerator FadeOut()
    {
        var startColor = isBlack ? new Color(0, 0, 0, 1) : new Color(1, 1, 1, 1);
        
        fadeImage.color = startColor;
        
        // fadeOutTime 동안 fadeImage의 alpha값을 0으로 변경
        var time = 0f;
        while (time < fadeOutTime)
        {
            fadeImage.color = isBlack
                ? new Color(0, 0, 0, Mathf.Lerp(1, 0, time / fadeOutTime))
                : new Color(1, 1, 1, Mathf.Lerp(1, 0, time / fadeOutTime));
            
            time += Time.deltaTime;
            
            yield return null;
        }
        
        if (fadeType is Enums.FadeType.Out or Enums.FadeType.InOut or Enums.FadeType.OutIn)
        {
            // fadeOutEventWaitTime 동안 대기
            yield return new WaitForSeconds(fadeOutEventWaitTime);
            
            onCompleteFadeOut?.Invoke();
        }
        
        if (fadeType == Enums.FadeType.OutIn)
        {
            // waitTime 동안 대기
            yield return new WaitForSeconds(waitTime);
            
            StartCoroutine(FadeIn());
        }
    }
}
