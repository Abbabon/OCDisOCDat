using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Bootstrapper : MonoBehaviour
{
    [Inject] private SceneManagerService _sceneManagerService;
    
    void Start()
    {
        _sceneManagerService.LoadNextScene(false);
    }
}
