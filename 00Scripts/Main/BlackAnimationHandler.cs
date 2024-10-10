using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAnimationHandler : MonoBehaviour
{
    private PlayerAnimationHandler playerAnimationHandler;
    

    private void Awake()
    {
        playerAnimationHandler = FindObjectOfType<PlayerAnimationHandler>();
    }

    public void OnCollider()
    {
        playerAnimationHandler.attackCollider.enabled = true;
    }
    
    public void OffCollider()
    {
        playerAnimationHandler.attackCollider.enabled = false;
    }
    
    public void OnComplete()
    {
        Player.canControl = true;
    }

    public void ShrinkOff()
    {
        playerAnimationHandler.ShrinkOff();
    }

    public void CheckNpc()
    {
        if(MapManager.I == null) return;
        
        //var mapCount = MapManager.I.GetMartMapDataListCount();
        var mapCount = MapManager.I.GetCurrentMapDataListCount();
        
        Debug.Log("mapCount: " + mapCount);

        if (mapCount == 1)
        {
            LogManager.I.GenerateLog("근처에서 움직이는 소리가 들렸다.");
        }
        else if (mapCount == 2)
        {
            LogManager.I.GenerateLog("어딘가에서 무슨 소리가 들린 것 같다.");
        }
        else if (mapCount >= 3)
        {
            LogManager.I.GenerateLog("이 안은 조용하다. 아무런 소리도 되돌아오지 않았다.");
        }
    }
}
