using UnityEngine;

public class PlayerShiftHandler : MonoBehaviour
{
    [SerializeField] private PlayerRaycastHandler whiteRaycastHandler;
    [SerializeField] private PlayerRaycastHandler blackRaycastHandler;
    
    private PlayerAnimationHandler playerAnimationHandler;
    private PortraitHandler portraitHandler;
    private CameraHandler cameraHandler;

    [SerializeField] private GameObject white;
    [SerializeField] private GameObject whiteToBlack;
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject blackToWhite;
    [SerializeField] private SpriteRenderer whiteRenderer;
    [SerializeField] private SpriteRenderer whiteToBlackRenderer;
    [SerializeField] private SpriteRenderer blackRenderer;
    [SerializeField] private SpriteRenderer blackToWhiteRenderer;
    [SerializeField] private Collider2D whiteCollider;
    [SerializeField] private Collider2D blackCollider;

    [SerializeField] private Transform whiteToBlackActivePoint;
    [SerializeField] private Transform blackActivePoint;
    [SerializeField] private Transform blackToWhiteActivePoint;
    [SerializeField] private Transform whiteActivePoint;

    private void Awake()
    {
        portraitHandler = FindObjectOfType<PortraitHandler>();
        playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
        cameraHandler = FindObjectOfType<CameraHandler>();
    }

    private void Update()
    {
        if (!Player.canControl)
            return;
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (Player.playerType)
            {
                case Enums.PlayerType.White:
                    if (!whiteRaycastHandler.Check(2 * white.transform.localScale.x, white.transform.localScale.x)) 
                        return;   
                    
                    //SoundManager.I.StopWhiteWalk();
                    SoundManager.I.PlaySFX_Switch();
                    
                    Debug.Log("Shift from White to Black");
                    Player.playerType = Enums.PlayerType.WhiteToBlack;
                    Player.canControl = false;
                    
                    //white.SetActive(false);
                    whiteRenderer.enabled = false;
                    whiteToBlack.transform.position = whiteToBlackActivePoint.position;
                    whiteToBlack.transform.localScale = white.transform.localScale;
                    whiteToBlack.SetActive(true);
                    //whiteToBlackRenderer.enabled = true;

                    whiteCollider.enabled = false;
                    blackCollider.enabled = false;
                    
                    //cameraHandler.ChangeFollow();
                    break;
                
                case Enums.PlayerType.Black:
                    if (blackRaycastHandler.Check(2 * black.transform.localScale.x, black.transform.localScale.x))
                        return;
                    
                    SoundManager.I.PlaySFX_Switch();
                    Player.playerType = Enums.PlayerType.BlackToWhite;
                    Player.canControl = false;
                    
                    //black.SetActive(false);
                    blackRenderer.enabled = false;
                    blackToWhite.transform.position = blackToWhiteActivePoint.position;
                    blackToWhite.transform.localScale = black.transform.localScale;
                    blackToWhite.SetActive(true);
                    //blackToWhiteRenderer.enabled = true;
                    
                    whiteCollider.enabled = false;
                    blackCollider.enabled = false;
            
                    //cameraHandler.ChangeFollow();
                    break;
                
                case Enums.PlayerType.WhiteToBlack:
                    break;
                
                case Enums.PlayerType.BlackToWhite:
                    break;
            }
        }
    }

    public void WhiteToBlackComplete()
    {
        whiteToBlack.SetActive(false);
        //whiteToBlackRenderer.enabled = false;
        black.transform.position = blackActivePoint.position;
        black.transform.localScale = white.transform.localScale;
        //black.SetActive(true);
        blackRenderer.enabled = true;
        
        whiteCollider.enabled = false;
        blackCollider.enabled = true;

        cameraHandler.ChangeFollow();

        portraitHandler.ChangePortrait(Enums.PlayerType.Black);
    }
    
    public void BlackToWhiteComplete()
    {
        blackToWhite.SetActive(false);
        //blackToWhiteRenderer.enabled = false;
        white.transform.position = whiteActivePoint.position;
        white.transform.localScale = black.transform.localScale;
        //white.SetActive(true);
        whiteRenderer.enabled = true;
        
        whiteCollider.enabled = true;
        blackCollider.enabled = false;
        
        cameraHandler.ChangeFollow();
        
        portraitHandler.ChangePortrait(Enums.PlayerType.White);
    }

    public void ShiftToBlack()
    {
        // white.transform, black.transform의 x값을 0으로 초기화
        white.transform.position = new Vector3(0, white.transform.position.y, white.transform.position.z);
        white.transform.localScale = new Vector3(-1, white.transform.localScale.y, white.transform.localScale.z);
        black.transform.position = new Vector3(0, black.transform.position.y, black.transform.position.z);
        black.transform.localScale = new Vector3(-1, black.transform.localScale.y, black.transform.localScale.z);
        
        if (Player.playerType != Enums.PlayerType.Black)
        {
            Player.playerType = Enums.PlayerType.WhiteToBlack;
            Player.canControl = false;


            //white.SetActive(false);
            whiteRenderer.enabled = false;
            whiteToBlack.transform.position = whiteToBlackActivePoint.position;
            whiteToBlack.transform.localScale = white.transform.localScale;
            whiteToBlack.SetActive(true);
            //whiteToBlackRenderer.enabled = true;

            whiteCollider.enabled = false;
            blackCollider.enabled = false;
                    
            //cameraHandler.ChangeFollow();
        }
            
    }
}
