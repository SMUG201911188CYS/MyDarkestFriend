using System;
using System.Collections.Generic;

public static class Player
{
    public static bool canControl = false;
    
    public static Enums.PlayerType playerType = Enums.PlayerType.White;
    
    public static Enums.InteractionType interactionType = Enums.InteractionType.None;
    
    public static ObjectInteractionHandler targetObject;

    public static bool isDiscovered = false;

    public static bool LockedDoor = false;

}