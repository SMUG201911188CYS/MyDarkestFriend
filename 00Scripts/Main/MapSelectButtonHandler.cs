using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MapSelectButtonHandler : HoverableButtonHandler
{

    [SerializeField] private Enums.MapSelectType type;
    public bool isActive = true;
    
    public async override void OnPointerClick()
    {
        switch (type)
        {
            case Enums.MapSelectType.Mart:
            case Enums.MapSelectType.School:
                MapManager.I.OnClickMapSelcetButton(type);
                break;
            default:
                Debug.Log("OnClick" + type);
                break;
        }
        
    }
    
    public override void OnPointerEnter()
    {
        if(isActive)
            MapManager.I.ChangeCurrentImage(type);
    }
    
    public override void OnPointerExit()
    {
        if(isActive) 
            MapManager.I.ChangeCurrentImage(Enums.MapSelectType.Base);
    }
}
