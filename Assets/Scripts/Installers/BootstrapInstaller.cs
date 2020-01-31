using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private SceneManagerService _sceneManagerService;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_playerScore).AsSingle().NonLazy();
        Container.BindInstance(_sceneManagerService).AsSingle().NonLazy();
    }
}
