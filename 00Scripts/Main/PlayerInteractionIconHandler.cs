using UnityEngine;

public class PlayerInteractionIconHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer whiteInteractionIcon;
    [SerializeField] private SpriteRenderer blackInteractionIcon;
    
    [SerializeField] private Sprite selectIcon;
    [SerializeField] private Sprite destroyIcon;
    [SerializeField] private Sprite moveIcon;
    
    private void Start()
    {
        InactiveIcon();
    }

    public void SetIcon()
    {
        switch (Player.interactionType)
        {
            case Enums.InteractionType.None:
                InactiveIcon();
                break;
            
            case Enums.InteractionType.Select:

                whiteInteractionIcon.transform.position = Player.targetObject.interactionIconPosition;
                blackInteractionIcon.transform.position = Player.targetObject.interactionIconPosition;
                
                whiteInteractionIcon.sprite = selectIcon;
                blackInteractionIcon.sprite = selectIcon;
                
                ActiveIcon();
                break;
            
            case Enums.InteractionType.Destroy:
                whiteInteractionIcon.sprite = destroyIcon;
                blackInteractionIcon.sprite = destroyIcon;
                
                ActiveIcon();
                break;
            
            case Enums.InteractionType.Move:
                whiteInteractionIcon.sprite = moveIcon;
                blackInteractionIcon.sprite = moveIcon;
                
                ActiveIcon();
                break;
        }
    }
    
    public void ActiveIcon()
    {
        if (Player.playerType == Enums.PlayerType.White)
        {
            whiteInteractionIcon.enabled = true;
            blackInteractionIcon.enabled = false;
        }
        else if (Player.playerType == Enums.PlayerType.Black)
        {
            whiteInteractionIcon.enabled = false;
            blackInteractionIcon.enabled = true;
        }
        else
        {
            whiteInteractionIcon.enabled = false;
            blackInteractionIcon.enabled = false;
        }
    }
    
    public void InactiveIcon()
    {
        whiteInteractionIcon.enabled = false;
        blackInteractionIcon.enabled = false;
    }
}
