using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class Level3Installer : MonoInstaller
{
    [SerializeField] private TappableShoe[] _tappableShoes;
    [SerializeField] private LevelResolver _levelResolver;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_tappableShoes).AsSingle().NonLazy();
        Container.BindInstance(_levelResolver).AsSingle().NonLazy();
    }
}