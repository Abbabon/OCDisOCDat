using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DraggableShoe : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;

    protected override void OnDrag(Transform draggedTransform)
    {
        _transform.localPosition = new Vector3(draggedTransform.localPosition.x, draggedTransform.localPosition.y, _transform.localPosition.z);

        ChangeInputState(false);
        _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.SlippersLevel1));
    }
}