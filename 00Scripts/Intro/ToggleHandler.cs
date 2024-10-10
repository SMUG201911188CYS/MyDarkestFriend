using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHandler : MonoBehaviour
{
    [SerializeField] private GameObject hideImage;

    public bool isOn;
    
    public virtual void Toggle()
    {
        isOn = !isOn;
        hideImage.SetActive(!isOn);
    }
}
