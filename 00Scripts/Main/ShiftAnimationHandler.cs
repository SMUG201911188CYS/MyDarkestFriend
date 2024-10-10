using UnityEngine;

public class ShiftAnimationHandler : MonoBehaviour
{
    private PlayerShiftHandler playerShiftHandler;
    private PlayerMovementHandler playerMovementHandler;
    
    private void Awake()
    {
        playerShiftHandler = FindObjectOfType<PlayerShiftHandler>();
        playerMovementHandler = FindObjectOfType<PlayerMovementHandler>();
    }

    public void WhiteToBlackComplete()
    {
        Player.playerType = Enums.PlayerType.Black;
        Player.canControl = true;
        
        playerShiftHandler.WhiteToBlackComplete();
        
        playerMovementHandler.ChangePlayer();
    }
    
    public void BlackToWhiteComplete()
    {
        Player.playerType = Enums.PlayerType.White;
        Player.canControl = true;
        
        playerShiftHandler.BlackToWhiteComplete();
        
        playerMovementHandler.ChangePlayer();
    }
}
