using RSG;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerService : MonoBehaviour
{
    //starting scene 0 is bootstrap scene
    private int _currentLevelIndex = 0;
    
    public void LoadNextScene(bool unloadCurrentScene)
    {
        if (unloadCurrentScene)
        {
            SceneManager.UnloadSceneAsync(_currentLevelIndex);
        }
        
        _currentLevelIndex++;
        if (!SceneManager.GetSceneByBuildIndex(_currentLevelIndex).isLoaded)
        {
            SceneManager.LoadScene(_currentLevelIndex, LoadSceneMode.Additive);
        }
    }
}