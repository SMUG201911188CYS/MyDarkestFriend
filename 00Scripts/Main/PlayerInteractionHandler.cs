using System;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    private PlayerInteractionIconHandler playerInteractionIconHandler;
    private PlayerAnimationHandler playerAnimationHandler;

    private void Awake()
    {
        playerInteractionIconHandler = FindObjectOfType<PlayerInteractionIconHandler>();
        playerAnimationHandler = FindObjectOfType<PlayerAnimationHandler>();
    }

    private void Update()
    {
        if (!Player.canControl)
            return;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
        {
            // 움크리기 or 움크리기 해제
            playerAnimationHandler.Shrink();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // 소리내기
            playerAnimationHandler.Sound();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnimationHandler.Attack();
        }

        if (Player.interactionType == Enums.InteractionType.None)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Player.interactionType != Enums.InteractionType.Select)
                return;
            
            if (Player.targetObject == null)
                return;
            
            if (Player.targetObject.objectType == Enums.ObjectType.Door)
            {
                Debug.Log($"{Player.targetObject.name} is selected");
                Player.targetObject.Interact();
            }
            else if (Player.targetObject.objectType == Enums.ObjectType.Item)
            {
                Debug.Log($"{Player.targetObject.name} is selected");
                Player.targetObject.Interact();
            }
            else
            {
                Player.interactionType = Enums.InteractionType.Destroy;
                playerInteractionIconHandler.SetIcon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Player.interactionType != Enums.InteractionType.Destroy)
                return;
            
            Player.interactionType = Enums.InteractionType.None;
            playerInteractionIconHandler.InactiveIcon();
                
            if(Player.targetObject != null)
                Player.targetObject.DestroyObject();
        }
    }
}

/*

else if (Player.targetObject.objectType is Enums.ObjectType.C or Enums.ObjectType.D or Enums.ObjectType.E)
{
    Player.interactionType = Enums.InteractionType.Destroy;
    playerInteractionIconHandler.SetIcon();
}

if (Player.interactionType != Enums.InteractionType.Select)
    return;

if (Player.targetObject == null)
    return;
    
if (Player.targetObject.objectType is Enums.ObjectType.A or Enums.ObjectType.B1 or Enums.ObjectType.B2 or Enums.ObjectType.F or Enums.ObjectType.G)
{
    Player.interactionType = Enums.InteractionType.Destroy;
    playerInteractionIconHandler.SetIcon();
}

*/