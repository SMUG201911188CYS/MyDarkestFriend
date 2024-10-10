using UnityEngine;
using UnityEngine.UI;

public class MinimapHandler : MonoBehaviour
{
    [Header("A Blocks")]
    [SerializeField] private Image aBlock1;
    [SerializeField] private Image aBlock2;
    [SerializeField] private Image aBlock3;
    
    [Header("B Blocks")]
    [SerializeField] private Image bBlock1;
    [SerializeField] private Image bBlock2;
    [SerializeField] private Image bBlock3;
    
    [Header("C Blocks")]
    [SerializeField] private Image cBlock1;
    [SerializeField] private Image cBlock2;
    [SerializeField] private Image cBlock3;
    
    [Header("D Blocks")]
    [SerializeField] private Image dBlock1;
    [SerializeField] private Image dBlock2;
    [SerializeField] private Image dBlock3;

    [Header("A to B Lines")]
    [SerializeField] private Image aLineUp1To2;
    [SerializeField] private Image aLineRight1To1;
    [SerializeField] private Image aLineUp2To3;
    [SerializeField] private Image aLineRight2To2;
    [SerializeField] private Image aLineDown2To1;
    [SerializeField] private Image aLineRight3To3;
    [SerializeField] private Image aLineDown3To2;
    
    [Header("B to C Lines")]
    [SerializeField] private Image bLineUp1To2;
    [SerializeField] private Image bLineRight1To1;
    [SerializeField] private Image bLineUp2To3;
    [SerializeField] private Image bLineRight2To2;
    [SerializeField] private Image bLineDown2To1;
    [SerializeField] private Image bLineRight3To3;
    [SerializeField] private Image bLineDown3To2;
    
    [Header("C to D Lines")]
    [SerializeField] private Image cLineUp1To2;
    [SerializeField] private Image cLineRight1To1;
    [SerializeField] private Image cLineUp2To3;
    [SerializeField] private Image cLineRight2To2;
    [SerializeField] private Image cLineDown2To1;
    [SerializeField] private Image cLineRight3To3;
    [SerializeField] private Image cLineDown3To2;
    
    
    [Header("Sprites")]
    [SerializeField] private Sprite unselectedMapSprite;
    [SerializeField] private Sprite passedMapSprite;
    [SerializeField] private Sprite currentMapSprite;

    private int preSelectedBlock = 1;

    public void Init()
    {
        // 모든 블록을 초기화
        aBlock1.sprite = unselectedMapSprite;
        aBlock2.sprite = unselectedMapSprite;
        aBlock3.sprite = unselectedMapSprite;
        
        bBlock1.sprite = unselectedMapSprite;
        bBlock2.sprite = unselectedMapSprite;
        bBlock3.sprite = unselectedMapSprite;
        
        cBlock1.sprite = unselectedMapSprite;
        cBlock2.sprite = unselectedMapSprite;
        cBlock3.sprite = unselectedMapSprite;
        
        dBlock1.sprite = unselectedMapSprite;
        dBlock2.sprite = unselectedMapSprite;
        dBlock3.sprite = unselectedMapSprite;
        
        // 모든 라인을 비활성화
        aLineUp1To2.enabled = false;
        aLineRight1To1.enabled = false;
        aLineUp2To3.enabled = false;
        aLineRight2To2.enabled = false;
        aLineDown2To1.enabled = false;
        aLineRight3To3.enabled = false;
        aLineDown3To2.enabled = false;
        
        bLineUp1To2.enabled = false;
        bLineRight1To1.enabled = false;
        bLineUp2To3.enabled = false;
        bLineRight2To2.enabled = false;
        bLineDown2To1.enabled = false;
        bLineRight3To3.enabled = false;
        bLineDown3To2.enabled = false;
        
        cLineUp1To2.enabled = false;
        cLineRight1To1.enabled = false;
        cLineUp2To3.enabled = false;
        cLineRight2To2.enabled = false;
        cLineDown2To1.enabled = false;
        cLineRight3To3.enabled = false;
        cLineDown3To2.enabled = false;
    }
    
    public void SelectA(int block)
    {
        aBlock1.sprite = unselectedMapSprite;
        aBlock2.sprite = unselectedMapSprite;

        switch (block)
        {
            case 1:
                aBlock1.sprite = currentMapSprite;
                break;
            
            case 2:
                aBlock2.sprite = currentMapSprite;
                break;
        }
        
        preSelectedBlock = block;
    }
    
    public void SelectB(int block)
    {
        bBlock1.sprite = unselectedMapSprite;
        bBlock2.sprite = unselectedMapSprite;
        
        aLineRight1To1.enabled = false;
        aLineUp1To2.enabled = false;
        aLineRight2To2.enabled = false;
        aLineDown2To1.enabled = false;

        switch (block)
        {
            case 1:
                bBlock1.sprite = currentMapSprite;

                if (preSelectedBlock == 1)
                {
                    aLineRight1To1.enabled = true;
                }
                else if (preSelectedBlock == 2)
                {
                    aLineDown2To1.enabled = true;
                }
                break;
            
            case 2:
                bBlock2.sprite = currentMapSprite;
                
                if (preSelectedBlock == 1)
                {
                    aLineUp1To2.enabled = true;
                }
                else if (preSelectedBlock == 2)
                {
                    aLineRight2To2.enabled = true;
                }
                break;
        }
        
        switch (preSelectedBlock)
        {
            case 1:
                aBlock1.sprite = passedMapSprite;
                break;
            
            case 2:
                aBlock2.sprite = passedMapSprite;
                break;
        }
        
        preSelectedBlock = block;
    }
    
    public void SelectC(int block)
    {
        cBlock1.sprite = unselectedMapSprite;
        cBlock2.sprite = unselectedMapSprite;
        
        bLineRight1To1.enabled = false;
        bLineUp1To2.enabled = false;
        bLineRight2To2.enabled = false;
        bLineDown2To1.enabled = false;

        switch (block)
        {
            case 1:
                cBlock1.sprite = currentMapSprite;

                if (preSelectedBlock == 1)
                {
                    bLineRight1To1.enabled = true;
                }
                else if (preSelectedBlock == 2)
                {
                    bLineDown2To1.enabled = true;
                }
                break;
            
            case 2:
                cBlock2.sprite = currentMapSprite;
                
                if (preSelectedBlock == 1)
                {
                    bLineUp1To2.enabled = true;
                }
                else if (preSelectedBlock == 2)
                {
                    bLineRight2To2.enabled = true;
                }
                break;
        }
        
        switch (preSelectedBlock)
        {
            case 1:
                bBlock1.sprite = passedMapSprite;
                break;
            
            case 2:
                bBlock2.sprite = passedMapSprite;
                break;
        }
        
        preSelectedBlock = block;
    }
    
    public void SelectD(int block)
    {
        dBlock1.sprite = unselectedMapSprite;
        dBlock2.sprite = unselectedMapSprite;
        
        cLineRight1To1.enabled = false;
        cLineUp1To2.enabled = false;
        cLineRight2To2.enabled = false;
        cLineDown2To1.enabled = false;

        switch (block)
        {
            case 1:
                dBlock1.sprite = currentMapSprite;

                if (preSelectedBlock == 1)
                {
                    cLineRight1To1.enabled = true;
                }
                else if (preSelectedBlock == 2)
                {
                    cLineDown2To1.enabled = true;
                }
                break;
            
            case 2:
                dBlock2.sprite = currentMapSprite;
                
                if (preSelectedBlock == 1)
                {
                    cLineUp1To2.enabled = true;
                }
                else if (preSelectedBlock == 2)
                {
                    cLineRight2To2.enabled = true;
                }
                break;
        }
        
        switch (preSelectedBlock)
        {
            case 1:
                cBlock1.sprite = passedMapSprite;
                break;
            
            case 2:
                cBlock2.sprite = passedMapSprite;
                break;
        }
        
        preSelectedBlock = block;
    }
}
