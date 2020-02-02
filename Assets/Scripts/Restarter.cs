
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Restarter : MonoBehaviour
{
    private SceneManagerService _sceneManagerService;
    private SoundService _soundService;
    
    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, SoundService soundService)
    {
        _sceneManagerService = sceneManagerService;
        _soundService = soundService;
    }
    
    public void RestartGame()
    {
        _soundService.PlayGameMusic();
        _sceneManagerService.LoadScene(ScenesEnum.GameStart);
        _sceneManagerService.UnloadScene(ScenesEnum.Credits);
    }    
    
}
