using System;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectInteractionHandler : MonoBehaviour
{
    public Enums.ObjectType objectType;

    public Vector2 interactionIconPosition;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player.targetObject = null;
        
        if (col.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter2D");
            
            if (Player.targetObject == null)
            {
                Player.targetObject = this;
                
                Debug.Log("Player.targetObject is set to " + this.name);

                if (Player.targetObject.objectType == Enums.ObjectType.Item)
                {
                    if (Player.playerType == Enums.PlayerType.White) SetIcon();
                }
                else
                {
                    SetIcon();
                }
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("OnTriggerExit2D");
            
            if (Player.targetObject == this)
            {
                Player.targetObject = null;
                
                Debug.Log("Player.targetObject is set to null");
                
                ObjectManager.I.playerInteractionIconHandler.InactiveIcon();
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("OnTriggerStay2D");
            
            if (Player.targetObject == null)
            {
                Player.targetObject = this;
                
                Debug.Log("Player.targetObject is set to " + this.name);
                
                SetIcon();
            }
        }
    }

    private void SetIcon()
    {
        Player.interactionType = Enums.InteractionType.Select;
        ObjectManager.I.playerInteractionIconHandler.SetIcon();
    }

    public virtual void DestroyObject()
    {
        Debug.Log("Destroy Object " + this.name);

        SoundManager.I.PlaySFX_Crack();
        FindObjectOfType<CameraHandler>().Shake();
        
        Player.targetObject = null;
    }

    public virtual void Interact()
    {
    }
}
