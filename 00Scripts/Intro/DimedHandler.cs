using System;

public class DimedHandler : FadeHandler
{
    private void OnEnable()
    {
        base.Start();
        if (Main.I != null)
            Main.I.canPause = false;
        fadeImage.raycastTarget = true;
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
