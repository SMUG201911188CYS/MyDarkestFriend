using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrollHandler : MonoBehaviour
{
    public Image img;
    private float offset;
    
    public float speed;
    
    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        img.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
