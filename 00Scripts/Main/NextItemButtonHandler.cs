using UnityEngine;
using UnityEngine.UI;

public class NextItemButtonHandler : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        BuyItemManager.I.Next();
    }
}
