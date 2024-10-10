using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationHandler : MonoBehaviour
{
    private Animator objAnimator;

    public string[] clipName;
    private void Awake()
    {
        TryGetComponent(out objAnimator);
        objAnimator.enabled = false;
        
    }
    
    public void PlayAnimation(string clip)
    {
        objAnimator.enabled = true;
        
        objAnimator.Play(clip);
    }

    public void StopAnimation()
    {
        objAnimator.enabled = false;
    }
}
