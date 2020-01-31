using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Inject] private SceneManagerService _sceneManagerService;
    
    void Start()
    {
        _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.GameStarter);   
    }
}
