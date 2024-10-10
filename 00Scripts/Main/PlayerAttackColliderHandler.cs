using System;
using UnityEngine;

public class PlayerAttackColliderHandler : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("NPC"))
        {
            SoundManager.I.PlaySFX_SuccessEat();
            var npc = col.transform.parent.GetComponent<NpcStateHandler>(); 
            npc.Die();
            StatManager.I.ChangePlayerStat(Enums.PlayerStat.Nature, 1f);
            StatManager.I.ChangePlayerStat(Enums.PlayerStat.Acquisition, 0.5f);
            
            if(StatManager.I.eatNpcCount > 3)
                StatManager.I.ChangePlayerStat(Enums.PlayerStat.Risk, 1f);
        }
    }
}
