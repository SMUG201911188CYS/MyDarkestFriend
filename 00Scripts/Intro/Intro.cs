using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private FadeHandler dimed;
    
    [SerializeField] private List<Button> clickButtons;
    private void Start()
    {
        title.SetActive(true);
        
        SoundManager.I.PlayBGM_Title();
        
        foreach (var button in clickButtons)
        {
            button.onClick.AddListener(() => SoundManager.I.PlaySFX_Click());
        }
    }
 
    
        // Start Game //
    
    // Saved Game Start
    public void SavedGameStart()
    {
        dimed.onCompleteFadeIn.AddListener(() =>Scene.LoadScene(Enums.SceneType.Main));
        
        dimed.gameObject.SetActive(true);
    }
    
    // New Game Start
    public void NewGameStart()
    {
        dimed.onCompleteFadeIn.AddListener(() =>Scene.LoadScene(Enums.SceneType.Tutorial));
        
        dimed.gameObject.SetActive(true);
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
}
