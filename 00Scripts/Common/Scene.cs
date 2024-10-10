using System;
using UnityEngine.SceneManagement;

public static class Scene
{
    public static void LoadScene(Enums.SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString());
    } 
}
