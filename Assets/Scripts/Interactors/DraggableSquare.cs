using UnityEngine;
using Zenject;

public class DraggableSquare : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;

    protected override void OnDrag()
    {
        Debug.Log("here!");
        _sceneManagerService.LoadNextScene(true);
    }
}