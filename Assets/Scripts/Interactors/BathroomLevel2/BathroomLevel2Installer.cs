using Zenject;
using UnityEngine;

public class BathroomLevel2Installer : MonoInstaller
{
    [SerializeField] private LevelResolver _levelResolver;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_levelResolver).AsSingle().NonLazy();
    }
}