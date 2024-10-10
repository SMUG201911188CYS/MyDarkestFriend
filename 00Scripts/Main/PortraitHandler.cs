using TMPro;
using UnityEngine;

public class PortraitHandler : MonoBehaviour
{
    public Enums.PlayerType playerType;

    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private TextMeshProUGUI playerNameText;
    
    private void Start()
    {
        SetPortrait();
    }
    
    public void SetPortrait()
    {
        switch (playerType)
        {
            case Enums.PlayerType.White:
                CheckBranch();
                break;
            case Enums.PlayerType.Black:
                portraitAnimator.Play("B_Portrait");
                
                playerNameText.text = "<color=black>Black</color>";
                break;
        }
    }
    
    public void ChangePortrait(Enums.PlayerType playerType)
    {
        this.playerType = playerType;
        
        SetPortrait();
    }
    
    private void CheckBranch()
    {
        if (!DateManager.I)
        {
            portraitAnimator.Play("W_Portrait_Branch_1");
            playerNameText.text = "<color=white>White</color>";
            return;
        }
        
        switch (StatManager.I.growthQuarter)
        {
            case 1 : // 1분기
                portraitAnimator.Play("W_Portrait_Branch_1");
                break;
            case 2 : // 2분기
                portraitAnimator.Play("W_Portrait_Branch_2");
                break;
            default: // 추후 분기 별 추가
                portraitAnimator.Play("W_Portrait_Branch_1");
                break;
                
        }
        
        playerNameText.text = "<color=white>White</color>";
    }
}
