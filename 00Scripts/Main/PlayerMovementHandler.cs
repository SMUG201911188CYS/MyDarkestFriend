using System;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private PlayerRaycastHandler whiteRaycastHandler;
    [SerializeField] private PlayerRaycastHandler blackRaycastHandler;
    
    
    [SerializeField] private float whiteMoveSpeed = 1f;
    [SerializeField] private float blackMoveSpeed = 1.5f;
    private float moveSpeed;
    
    private PlayerAnimationHandler playerAnimationHandler;
    
    [SerializeField] private Transform white;
    [SerializeField] private Transform black;
    public Transform currentPlayer;
    private void Awake()
    {
        playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
        
        currentPlayer = white;
        moveSpeed = whiteMoveSpeed;
        
        Player.canControl = false;
        Player.interactionType = Enums.InteractionType.None;
        Player.playerType = Enums.PlayerType.White;
        Player.targetObject = null;
    }
    
    private void FixedUpdate()
    {
        if (!Player.canControl)
        {
            playerAnimationHandler.Stop();
            return;
        }
        
        if (currentPlayer == null)
            return;
        
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(horizontal, 0), .3f, LayerMask.GetMask("Wall"));
            
            bool canMove = false;
            if (currentPlayer == white)
                canMove = !whiteRaycastHandler.Check(horizontal * 0.2f, white.transform.localScale.x);
            else if (currentPlayer == black)
                canMove = blackRaycastHandler.Check(horizontal * 0.65f, black.transform.localScale.x);
            
            if (hit.collider != null)
                return;
            
            if (!canMove)
                return;
            
            if (currentPlayer == white)
                SoundManager.I.PlaySFX_WhiteWalk();
            
            playerAnimationHandler.Move();
            Vector3 moveDirection = new Vector3(horizontal, 0, 0).normalized;
        
            currentPlayer.position += moveDirection * (moveSpeed * Time.deltaTime);
            
            CheckDirection(horizontal);
        }
        else
        {
            playerAnimationHandler.Stop();
            
            SoundManager.I.StopWhiteWalk();
        }
    }
    
    private void CheckDirection(float horizontal)
    {
        if (horizontal > 0)
        {
            currentPlayer.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontal < 0)
        {
            currentPlayer.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    public void ChangePlayer()
    {
        switch (Player.playerType)
        {
            case Enums.PlayerType.White:
                currentPlayer = white;
                moveSpeed = whiteMoveSpeed;
                break;
            
            case Enums.PlayerType.Black:
                currentPlayer = black;
                moveSpeed = blackMoveSpeed;
                break;
            
            case Enums.PlayerType.WhiteToBlack:
            case Enums.PlayerType.BlackToWhite:
                currentPlayer = null;
                break;
        }
    }
    
    public void InitPosition()
    {
        white.localPosition = new Vector3(0, 0, 0);
        white.localScale = new Vector3(-1, 1, 1);
        black.localPosition = new Vector3(1.15f, 0.5f, 0);
        black.localScale = new Vector3(-1, 1, 1);
    }

    public void ChangeMapPosition()
    {
        white.localPosition = new Vector3(-9.2f, 0, 0);
        white.localScale = new Vector3(1, 1, 1);
        black.localPosition = new Vector3(-9.2f, 0.5f, 0);
        black.localScale = new Vector3(1, 1, 1);
        
    }
}
