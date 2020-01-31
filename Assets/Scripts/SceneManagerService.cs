using RSG;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerService : MonoBehaviour
{
    public void UnloadScene(ScenesEnum scene)
    {
        SceneManager.UnloadSceneAsync((int)scene);   
    }
    
    public void LoadScene(ScenesEnum scene)
    {
        if (!SceneManager.GetSceneByBuildIndex((int)scene).isLoaded)
        {
            SceneManager.LoadScene((int)scene, LoadSceneMode.Additive);
        }
    }
    
    public void UnloadSceneAndLoadNext(ScenesEnum scene)
    {
        UnloadScene(scene);
        LoadScene(scene+1);
    }
}