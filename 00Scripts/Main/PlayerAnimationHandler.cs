using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator whiteAnimator;
    [SerializeField] private Animator blackAnimator;

    [SerializeField] private Animator whiteShiftAnimator;
    [SerializeField] private Animator blackShiftAnimator;
    
    
    
    public Collider2D attackCollider;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsShrink = Animator.StringToHash("IsShrink");
    private static readonly int IsSounding = Animator.StringToHash("IsSounding");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    public void ChangeAnimator()
    {
        switch (StatManager.I.growthQuarter)
        {
            case 1:
                whiteAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player W");
                whiteShiftAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player Shift W to B");
                blackShiftAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player Shift B to W");
                break;
            case 2:
                whiteAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player W 2");
                whiteShiftAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player Shift W2 to B2");
                blackShiftAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player Shift B2 to W2");
                break;
            default:
                whiteAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player W");
                whiteShiftAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player Shift W to B");
                blackShiftAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Arts/Motion/White/Player Shift B to W");
                break;
        }
    }
    
    public void Move()
    {
        switch (Player.playerType)
        {
            case Enums.PlayerType.White:
                whiteAnimator.SetBool(IsMoving, true);
                break;

            case Enums.PlayerType.Black:
                blackAnimator.SetBool(IsMoving, true);
                break;
            
            case Enums.PlayerType.WhiteToBlack:
                whiteAnimator.SetBool(IsMoving, false);
                break;
            case Enums.PlayerType.BlackToWhite:
                blackAnimator.SetBool(IsMoving, false);
                break;
        }
    }
    
    public void Stop()
    {
        switch (Player.playerType)
        {
            case Enums.PlayerType.White:
                whiteAnimator.SetBool(IsMoving, false);
                break;
            
            case Enums.PlayerType.Black:
                blackAnimator.SetBool(IsMoving, false);
                break;
            
            case Enums.PlayerType.WhiteToBlack:
                whiteAnimator.SetBool(IsMoving, false);
                break;
                
            case Enums.PlayerType.BlackToWhite:
                blackAnimator.SetBool(IsMoving, false);
                break;
        }
    }

    // 움크리기
    public void Shrink()
    {
        switch (Player.playerType)
        {
            case Enums.PlayerType.White:
                break;

            case Enums.PlayerType.Black:
                blackAnimator.SetBool(IsShrink, !blackAnimator.GetBool(IsShrink));

                Player.canControl = false;
                break;
        }
    }
    
    // 소리내기
    public void Sound()
    {
        switch (Player.playerType)
        {
            case Enums.PlayerType.White:
                break;

            case Enums.PlayerType.Black:
                if (blackAnimator.GetBool(IsShrink))
                    return;
                
                blackAnimator.SetTrigger(IsSounding);
                
                SoundManager.I.PlaySFX_Grrr();
                
                Player.canControl = false;
                break;
        }
    }

    public void Attack()
    {
        switch (Player.playerType)
        {
            case Enums.PlayerType.White:
                break;
            case Enums.PlayerType.Black:
                if (!blackAnimator.GetBool(IsShrink))
                    return;
                
                blackAnimator.SetTrigger(IsAttack);
                
                Player.canControl = false;
                break;
        }
    }

    public void ShrinkOff()
    {
        blackAnimator.SetBool(IsShrink, false);
        Player.canControl = true;
    }
}
