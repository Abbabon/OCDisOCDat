using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControlsInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_mainCamera).AsSingle().NonLazy();
    }
}
