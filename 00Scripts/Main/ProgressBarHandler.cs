using UnityEngine;

public class ProgressBarHandler : MonoBehaviour
{
    [SerializeField] private Transform progressBar;
    
    public virtual void SetProgress(float percent)
    {
        progressBar.localScale = new Vector3(percent / 100, 1, 1);
    }
}
