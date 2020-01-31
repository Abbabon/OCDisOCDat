using Zenject;
using UnityEngine;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private DragTarget _draggableTarget;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_draggableTarget).AsSingle().NonLazy();
    }
}