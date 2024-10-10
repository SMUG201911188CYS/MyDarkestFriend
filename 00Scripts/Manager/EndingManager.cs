using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EndingManager : MonoBehaviour
{
    public static EndingManager I;
    [SerializeField] private GameObject endUI;
    [SerializeField] private List<Ending> endingList;
    
    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(EndingManager)) as EndingManager;
        }
    }

    public bool CheckEnd()
    {
        
        foreach (var end in endingList)
        {
            for (int i = 0; i < end.endingCondition.Length; i++)
            {
                if ( StatManager.I.GetPlayerStat(end.endingCondition[i].condition) >= end.endingCondition[i].conditionLimit)
                {
                    PlayEnding(end);
                    return true;
                }
            }
        }
        
        return false;
    }
    
    public async void PlayEnding(Ending end)
    {
        FindObjectOfType<ObjectAnimationHandler>().PlayAnimation(end.endingAnimClip.name);
        
        await Task.Delay(2000);
        
        foreach (var line in end.script)
        {
            var speaker = line.Split(':')[0];

            if (speaker == "W")
            {
                LogManager.I.portraitHandler.ChangePortrait(Enums.PlayerType.White);
            }
            else if (speaker == "B")
            {
                LogManager.I.portraitHandler.ChangePortrait(Enums.PlayerType.Black);
            }
            else
            {
                LogManager.I.portraitHandler.ChangePortrait(Enums.PlayerType.White);
            }
            
            LogManager.I.GenerateLog(line);
            
            await Task.Delay(2000);
        }
        
        Main.I.ActiveFadeInDimed();
        await Task.Delay(1500);
        
        SoundManager.I.PlaySFX_Gun();
        await Task.Delay(1500);
        
        endUI.SetActive(true);
        SoundManager.I.PlaySFX_Gun();
        
        while (true)
        {
            if (Input.anyKeyDown)
            {
                Scene.LoadScene(Enums.SceneType.Intro);
                break;
            }
            await UniTask.Delay(1);
        }
    }
}

[Serializable]
public class Ending
{
    public string endingName;
    public AnimationClip endingAnimClip;
    public EndingCondition[] endingCondition;
    public string[] script;
}

[Serializable]
public class EndingCondition
{
    public Enums.PlayerStat condition;
    public int conditionLimit;
}