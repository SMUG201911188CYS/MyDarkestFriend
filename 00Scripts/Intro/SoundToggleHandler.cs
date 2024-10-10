using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggleHandler : ToggleHandler
{
    [SerializeField] private Enums.SoundToggleType soundToggleType;
    
    public override void Toggle()
    {
        base.Toggle();
     
        if (soundToggleType == Enums.SoundToggleType.SFX)
        {
            MuteSFX();
        }
        else if (soundToggleType == Enums.SoundToggleType.BGM)
        {
            MuteBGM();
        }
    }
    
    private void MuteSFX()
    {
        Debug.Log($"MuteSFX : {isOn}");
        SoundManager.I.MuteSFX(isOn);
    }
    
    private void MuteBGM()
    {
        SoundManager.I.MuteBGM(isOn);
    }
}
