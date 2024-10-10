using System;
using UnityEngine;

public class ObjectHandler : ObjectInteractionHandler
{
    public Enums.ObjectState objectState;
    
    [SerializeField] private SpriteRenderer whiteSpriteRenderer;
    [SerializeField] private SpriteRenderer blackSpriteRenderer;
    [SerializeField] private SpriteMask lightSpriteMask;

    private Collider2D col;

    private bool isHasCol;
    
    private void OnEnable()
    {
        SetObjectState(Enums.ObjectState.Active);
    }

    private void Awake()
    {
        isHasCol = TryGetComponent(out col);
        SetObjectState(objectState);
    }

    private int lightIndex;
    
    private void Anim()
    {
        if (objectState != Enums.ObjectState.Active)
            return;
        
        lightIndex++;
        
        if (ObjectManager.I.objectGLightSpriteList.Count <= lightIndex)
            lightIndex = 0;
        
        lightSpriteMask.sprite = ObjectManager.I.objectGLightSpriteList[lightIndex];
        
        Invoke(nameof(Anim), 0.1f);
    }

    public override void DestroyObject()
    {
        objectState = Enums.ObjectState.Destroy;
        SetObjectState(objectState);

        col.enabled = false;
        
        base.DestroyObject();
    }

    public void SetObjectState(Enums.ObjectState objectState)
    {
        this.objectState = objectState;
        
        var _object = ObjectManager.I.objectList.Find(x => x.objectType == objectType);

        switch (objectState)
        {
            case Enums.ObjectState.Active:
                whiteSpriteRenderer.sprite = _object.whiteActiveSprite;
                blackSpriteRenderer.sprite = _object.blackActiveSprite;
                lightSpriteMask.sprite = _object.lightSprite;
                lightSpriteMask.enabled = true;
                
                if(isHasCol)
                    col.enabled = true;
                
                if (objectType is Enums.ObjectType.G || objectType is Enums.ObjectType.E)
                    Anim();
                
                break;
            
            case Enums.ObjectState.Inactive:
                whiteSpriteRenderer.sprite = _object.whiteInactiveSprite;
                blackSpriteRenderer.sprite = _object.blackInactiveSprite;
                lightSpriteMask.enabled = false;
                break;
            
            case Enums.ObjectState.Destroy:
                whiteSpriteRenderer.sprite = _object.whiteDestroySprite;
                blackSpriteRenderer.sprite = _object.blackDestroySprite;
                lightSpriteMask.enabled = false;
                col.enabled = false;
                break;
        }
        
        interactionIconPosition = transform.Find("Interaction Icon Position").position;
    }
    
    public override void Interact()
    {
        base.Interact();
        
        Debug.Log("Object Interact");
    }
}
