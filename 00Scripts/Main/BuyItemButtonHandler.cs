using UnityEngine;
using UnityEngine.UI;

public class BuyItemButtonHandler : MonoBehaviour
{
    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        BuyItemManager.I.SetBuyItemAsk();
    }
}
