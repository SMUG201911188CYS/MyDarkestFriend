using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private List<Cutscene> cutscenes;
    [SerializeField] private List<CutsceneAnim> cutsceneAnims;
    [SerializeField] private List<CutsceneAnimWithSpeed> cutsceneAnimsWithSpeeds;
    public GameObject walkSD;
    [SerializeField] Animator cutsceneAnimator;
    [SerializeField] Animator blackWalkAnimator;
    [SerializeField] Animator whiteWalkAnimator;
    
    public void SetSprite(string name)
    {
        foreach (var cutscene in cutscenes.Where(cutscene => cutscene.name == name))
        {
            cutsceneImage.sprite = cutscene.sprite;
            
            cg.alpha = 1;
            break;
        }
    }
    
    public void Hide()
    {
        if (cutsceneAnimator != null && cutsceneAnimator.enabled)
        {
            cutsceneAnimator.enabled = false;
        }
        
        if (blackWalkAnimator != null && blackWalkAnimator.enabled)
        {
            blackWalkAnimator.SetFloat("AnimSpeed", 0.5f);
            walkSD.SetActive(false);
        }
        
        if (whiteWalkAnimator != null && whiteWalkAnimator.enabled)
        {
            whiteWalkAnimator.SetFloat("AnimSpeed", 0.5f);
            walkSD.SetActive(false);
        }
        
        cg.alpha = 0;
    }

    public void PlayAnim(string name)
    {
        if (name is "천천히 걷는다." or "빨리 걷는다.")
        {
            var speed = cutsceneAnimsWithSpeeds.Find(x => x.name == name).speed;
            
            blackWalkAnimator.SetFloat("AnimSpeed", speed);
            whiteWalkAnimator.SetFloat("AnimSpeed", speed);
            
            return;
        }
        
        foreach (var cutsceneAnim in cutsceneAnims.Where(cutscene => cutscene.name == name))
        {
            cutsceneAnimator.enabled = true;
            cutsceneAnimator.Play(cutsceneAnim.clip.name);
            
            cg.alpha = 1;
            break;
        }
    }
}


[Serializable]
public class Cutscene
{
    public string name;
    public Sprite sprite;
}

[Serializable]

public class CutsceneAnim
{
    public string name;
    public AnimationClip clip;
}

[Serializable]

public class CutsceneAnimWithSpeed
{
    public string name;
    public float speed;
}