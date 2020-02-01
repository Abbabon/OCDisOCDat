using UnityEngine;
using Zenject;

//create a target scene in a common class
public class RotatableImageTwo : Rotatable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    private bool _locked = false;
    
    protected override void OnRotateTarget(float rotationAngle)
    {
        _transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        ChangeInputState(false);
        
        if (!_locked)
        {
            _locked = true;
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.Level7));
        }
    }
}