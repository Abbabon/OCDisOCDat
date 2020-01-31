using Zenject;
using UnityEngine;

public class Level2Installer : MonoInstaller
{
    [SerializeField] private TappableShoe _tappableShoe;
    [SerializeField] private LevelResolver _levelResolver;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_tappableShoe).AsSingle().NonLazy();
        Container.BindInstance(_levelResolver).AsSingle().NonLazy();
    }
}