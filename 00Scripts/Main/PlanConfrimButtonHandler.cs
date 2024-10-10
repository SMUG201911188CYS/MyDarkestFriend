using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanConfrimButtonHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI btnText;
    [SerializeField] private TextMeshProUGUI confirmText;
    private Button button;
    private Image img;
    
    private void Awake()
    {
        TryGetComponent(out button);
        TryGetComponent(out img);

        confirmText.enabled = false;
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        SoundManager.I.PlaySFX_Click();
        PlanManager.I.PlanEnd();
    }

    public void changeBtnState()
    {
        if (button.enabled)
        {
            img.color = new Color(1, 1, 1, 0.5f);
            button.enabled = false;
            confirmText.enabled = false;
        }
        else
        {
            img.color = new Color(1, 1, 1, 1);
            button.enabled = true;
            confirmText.enabled = true;
        }
    }
}
