using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public static LogManager I;

    [SerializeField] private RectTransform logParent;
    [SerializeField] private LogHandler logPrefab;

    [SerializeField] private CanvasGroup button3Cg;
    [SerializeField] private TextMeshProUGUI button3AText;
    [SerializeField] private TextMeshProUGUI button3BText;
    [SerializeField] private TextMeshProUGUI button3CText;
    [SerializeField] private CanvasGroup button2Cg;
    [SerializeField] private TextMeshProUGUI button2AText;
    [SerializeField] private TextMeshProUGUI button2BText;
    
    [SerializeField] private LogButtonHandler[] button3LogButtons;
    [SerializeField] private LogButtonHandler[] button2LogButtons;
    public PortraitHandler portraitHandler;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(LogManager)) as LogManager;
        }
        
        portraitHandler = FindObjectOfType<PortraitHandler>();

        // logParent 자식을 모두 제거
        for (var i = 0; i < logParent.childCount; i++)
        {
            Destroy(logParent.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        if (Player.canControl)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (button3Cg.blocksRaycasts)
            {
                button3LogButtons[0].OnPointerClick();
            }
            else if (button2Cg.blocksRaycasts)
            {
                button2LogButtons[0].OnPointerClick();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (button3Cg.blocksRaycasts)
            {
                button3LogButtons[1].OnPointerClick();
            }
            else if (button2Cg.blocksRaycasts)
            {
                button2LogButtons[1].OnPointerClick();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (button3Cg.blocksRaycasts)
            {
                button3LogButtons[2].OnPointerClick();
            }
        }
    }

    private void OnApplicationQuit()
    {
        // logParent 자식을 모두 제거
        for (var i = 0; i < logParent.childCount; i++)
        {
            Destroy(logParent.GetChild(i).gameObject);
        }
    }

    public async void GenerateLog(string logText)
    {
        if (!Application.isPlaying)
        {
            Debug.Log("Editor에서는 실행할 수 없습니다.");
            return;
        }
        
        SoundManager.I.PlaySFX_Text();
        
        var log = Instantiate(logPrefab, logParent);
        
        //Debug.Log(logText.Length);
        
        if(logText.Length > 57)
            log.SetSize();
        log.SetLog(logText);
            
        StartCoroutine(ScrollToBottomCoroutine());
    }
    
    public async void GenerateLogWithColor(string logText, Color logTextColor)
    {
        if (!Application.isPlaying)
        {
            Debug.Log("Editor에서는 실행할 수 없습니다.");
            return;
        }
        
        SoundManager.I.PlaySFX_Text();
        
        var log = Instantiate(logPrefab, logParent);
        
        if(logText.Length > 60)
            log.SetSize();
        log.SetLog(logText);
        log.SetColor(logTextColor);
            
        StartCoroutine(ScrollToBottomCoroutine());
    }

    private IEnumerator ScrollToBottomCoroutine()
    {
        // Content의 위치를 아래로 이동
        var scrollRect = logParent.parent.parent.GetComponent<ScrollRect>();
        
        logParent.anchoredPosition = new Vector2(logParent.anchoredPosition.x, 0f);
        
        // 잠시 대기하여 스크롤을 조정하고 스크립트가 실행될 때까지 기다림
        yield return null;

        // 스크롤뷰를 최하단으로 이동
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    
    public void Button3TextChange(string aText, string bText, string cText)
    {
        button3AText.text = aText;
        button3BText.text = bText;
        button3CText.text = cText;
        
        Button3Active();
    }
    
    public void Button2TextChange(string aText, string bText)
    {
        button2AText.text = aText;
        button2BText.text = bText;
        
        Button2Active();
    }
    
    
    
    public void Button3Active()
    {
        if (button2Cg.alpha != 0)
        {
            button2Cg.alpha = 0;
            button2Cg.blocksRaycasts = false;
        }
        
        button3Cg.DOFade(1, 0.5f).From(0).OnComplete(() =>
        {
            button3Cg.blocksRaycasts = true;
        });
    }
    
    public void Button3Inactive()
    {
        button3Cg.blocksRaycasts = false;

        button3Cg.DOFade(0, 0.5f);
    }
    
    public void Button2Active()
    {
        if (button3Cg.alpha != 0)
        {
            button3Cg.alpha = 0;
            button3Cg.blocksRaycasts = false;
        }
        
        button2Cg.DOFade(1, 0.5f).From(0).OnComplete(() =>
        {
            button2Cg.blocksRaycasts = true;
        });
    }
    
    public void Button2Inactive()
    {
        button2Cg.blocksRaycasts = false;

        button2Cg.DOFade(0, 0.5f);
    }
}
