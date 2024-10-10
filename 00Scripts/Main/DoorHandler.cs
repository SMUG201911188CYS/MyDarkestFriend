using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DoorHandler : ObjectInteractionHandler
{
    [SerializeField] private string doorName;
    
    public override void Interact()
    {
        base.Interact();
        
        Debug.Log("Door Interact");
        
        DoorManager.I.Interact(doorName);
    }
}
