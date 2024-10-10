using System;
using UnityEngine;

public class NpcCheckColliderHandler : MonoBehaviour
{
    private bool Flag = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Flag) return;
        if (!col.CompareTag("Player")) return;

        if (Player.playerType == Enums.PlayerType.Black)
        {
            Flag = true;
                
            transform.parent.GetComponent<NpcStateHandler>().ChangeToWarning();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Flag) return;
        if (!other.CompareTag("Player")) return;

        if (Player.playerType == Enums.PlayerType.Black)
        {
            Flag = true;
            
            transform.parent.GetComponent<NpcStateHandler>().ChangeToWarning();
        }
    }
}
