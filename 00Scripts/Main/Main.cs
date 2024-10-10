using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class Main : MonoBehaviour
{
    public static Main I;
    
    [SerializeField] private GameObject fadeInDimed;
    [SerializeField] private GameObject fadeOutDimed;
    [SerializeField] private GameObject fadeInOutDimed;

    [Header("Settings")]
    [SerializeField] private GameObject pauseBackground;
    [SerializeField] private GameObject pauseMain;
    [SerializeField] private GameObject pauseSetting;
    [SerializeField] private GameObject pauseVideo;
    [SerializeField] private GameObject pauseAudio;
    
    
    [SerializeField] private List<Button> clickButtons;
    
    public bool canPause = true;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(Main)) as Main;
        }
        
        foreach (var button in clickButtons)
        {
            button.onClick.AddListener(() => SoundManager.I.PlaySFX_Click());
        }
    }

    private void Start()
    {
        ActiveFadeOutDimed();
        
        SoundManager.I.PlayBGM_Main();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        Test();
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("Attention Change 99%");
            StatManager.I.ChangePlayerStat(Enums.PlayerStat.Attention, 99f);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log("Year + 1");
            DateManager.I.date.year += 1;
            DateManager.I.SetDateText();
            
            if(DateManager.I.date.year == 4)
            {
                StatManager.I.ChangeGrowthQuarter();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            if (DateManager.I.date.week >= 4)
            {
                Debug.Log("Week is Already 4");
                DateManager.I.date.week = 4;
                DateManager.I.SetDateText();
            }
            else
            {
                Debug.Log("Week + 1");
                DateManager.I.date.week += 1;
                DateManager.I.SetDateText();
            }
        }
    }
    public void ActiveFadeOutDimed()
    {
        fadeOutDimed.SetActive(true);
    }
    
    public void ActiveFadeInOutDimed()
    {
        canPause = false;
        fadeInOutDimed.SetActive(true);
    }
    
    public void ActiveFadeInDimed()
    {
        fadeInDimed.SetActive(true);
    }

    // Pause
    public void Pause()
    {
        // 애니메이션이나 코루틴이 동작 중이라면 실행 X
        if (!canPause)
            return;

        Time.timeScale = 0;
        
        pauseBackground.SetActive(true);
        pauseMain.SetActive(true);
        
        pauseSetting.SetActive(false);
        
        pauseVideo.SetActive(false);
        pauseAudio.SetActive(false);
    }
    
    public void SetCanPause(bool value)
    {
        canPause = value;
    }
    
    // Escape
    public void Settings()
    {
        Time.timeScale = 0;
        
        pauseBackground.SetActive(true);
        pauseMain.SetActive(false);
        
        pauseSetting.SetActive(true);
        
        pauseVideo.SetActive(false);
        pauseAudio.SetActive(false);
    }
    
    // Resume
    public void Resume()
    {
        Time.timeScale = 1;
        
        pauseBackground.SetActive(false);
        pauseMain.SetActive(false);
        
        pauseSetting.SetActive(false);
        
        pauseVideo.SetActive(false);
        pauseAudio.SetActive(false);
    }
    
    // Go To Intro
    public void GoToIntro()
    {
        Scene.LoadScene(Enums.SceneType.Intro);
        Time.timeScale = 1;
    }
    // Exit
    public void Exit()
    {
        // Editor인 경우 플레이 모드를 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Editor가 아닌 경우 애플리케이션 종료
        Application.Quit();
#endif
    }
    
    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        var constructor = SynchronizationContext.Current.GetType().GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(int)}, null);
        var newContext = constructor.Invoke(new object[] {Thread.CurrentThread.ManagedThreadId });
        SynchronizationContext.SetSynchronizationContext(newContext as SynchronizationContext); 
#endif
    }
}
