using TMPro;
using UnityEngine;

public class RiskProgressBarHandler : ProgressBarHandler
{
    [SerializeField] private TextMeshProUGUI percentText;

    public override void SetProgress(float percent)
    {
        base.SetProgress(percent);
        
        percentText.text = $"{percent}%";
    }
}
