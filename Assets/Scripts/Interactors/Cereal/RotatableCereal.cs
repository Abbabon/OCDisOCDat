using UnityEngine;
using Zenject;

public class RotatableCereal : Rotatable
{
    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;

    private bool _locked = false;
    
    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, IPromiseTimerService promiseTimerService, SoundService soundService)
    {
        _sceneManagerService = sceneManagerService;
        _promiseTimerService = promiseTimerService;
        _soundService = soundService;
    }
    
    protected override void OnRotateTarget(float rotationAngle)
    {
        _transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        ChangeInputState(false);
        
        if (!_locked)
        {
            _locked = true;
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Bad2);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.CerealLevel3));
        }
    }

    protected override void OnStartRotate()
    {
        _soundService.PlaySoundEffect(SoundService.SoundEffects.SpoonDrag);
    }
}