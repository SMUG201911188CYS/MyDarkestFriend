using UnityEngine;
using UnityEngine.UI;

public class ScheduleIconHandler : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetIcon(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
