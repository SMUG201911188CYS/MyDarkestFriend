using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager I;

    public List<Object> objectList;

    public List<Sprite> objectGLightSpriteList;
    
    public PlayerInteractionIconHandler playerInteractionIconHandler;

    private void Awake()
    {
        if (!I)
        {
            I = GameObject.FindObjectOfType(typeof(ObjectManager)) as ObjectManager;
        }
    }
}

[Serializable]
public class Object
{
    public Enums.ObjectType objectType;
    public Sprite blackActiveSprite;
    public Sprite whiteActiveSprite;
    public Sprite blackInactiveSprite;
    public Sprite whiteInactiveSprite;
    public Sprite blackDestroySprite;
    public Sprite whiteDestroySprite;
    public Sprite lightSprite;
}