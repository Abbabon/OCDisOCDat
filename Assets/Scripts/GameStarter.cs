using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStarter : MonoBehaviour
{
    private SoundService _soundService;
    private SceneManagerService _sceneManagerService;
    
    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, SoundService soundService)
    {
        _soundService = soundService;
        _sceneManagerService = sceneManagerService;
    }
    
    void Start()
    {
        _soundService.PlayGameMusic();
        
        #if !UNITY_EDITOR
        _sceneManagerService.LoadScene(ScenesEnum.GameStart);
        #endif
    }
}