using UnityEngine;
using Zenject;

public class RotatableImage : Rotatable
{
    private bool _locked = false;
    
    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;


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
            _soundService.PlaySoundEffect(SoundService.SoundEffects.PictureSwing);
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Good4);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.GalleryLevel1));
        }
    }

    protected override void OnStartRotate()
    {
        _soundService.PlaySoundEffect(SoundService.SoundEffects.PictureSwing);
    }
}